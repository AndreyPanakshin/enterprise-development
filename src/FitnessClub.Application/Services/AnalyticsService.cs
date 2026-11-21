using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис выполнения аналитических бизнес-запросов
/// </summary>
public class AnalyticsService(
    ITrainerRepository trainerRepo,
    IMemberRepository memberRepo,
    ITrainingSessionRepository sessionRepo,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <summary>Возвращает тренеров со стажем работы 5 лет и более</summary>
    public async Task<IList<TrainerDto>> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Аналитика: фильтрация тренеров со стажем от 5 лет");
        var trainers = await trainerRepo.GetWithMinExperience(5);
        return trainers
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }

    /// <summary>Проверяет, свободен ли указанный зал в заданное время</summary>
    public async Task<bool> CheckHallAvailability(string hallName, DateTime checkTime)
    {
        logger.LogInformation("Аналитика: проверка доступности зала '{Hall}' на время {Time}", hallName, checkTime);
        if (!await sessionRepo.HallExists(hallName))
        {
            throw new ArgumentException($"Зал '{hallName}' не зарегистрирован в системе.");
        }
        return !await sessionRepo.HasSessionInHall(hallName, checkTime);
    }

    /// <summary>Возвращает клиентов с просроченными абонементами</summary>
    public async Task<IList<MemberDto>> GetMembersWithExpiredSubscription()
    {
        logger.LogInformation("Аналитика: выгрузка просроченных абонементов");
        var today = DateOnly.FromDateTime(DateTime.Today);
        var members = await memberRepo.GetExpiredSubscriptions(today);
        return members
            .Select(c => new MemberDto(
                c.Id, c.PassportNumber, c.LastName, c.FirstName, c.Patronymic,
                c.Gender, c.DateOfBirth, c.PhoneNumber, c.MembershipStartDate, c.MembershipEndDate))
            .ToList();
    }

    /// <summary>Возвращает тренировки в указанном зале за текущий месяц</summary>
    public async Task<IList<PersonalTrainingSessionDto>> GetSessionsForCurrentMonthInSelectedHall(string hallName)
    {
        logger.LogInformation("Аналитика: занятия текущего месяца в зале '{Hall}'", hallName);
        if (!await sessionRepo.HallExists(hallName))
        {
            throw new ArgumentException($"Зал '{hallName}' не зарегистрирован в системе.");
        }
        var now = DateTime.Today;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var nextMonthStart = monthStart.AddMonths(1);
        var sessions = await sessionRepo.GetByHallAndPeriod(hallName, monthStart, nextMonthStart);
        return sessions
            .Select(s => new PersonalTrainingSessionDto(
                s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial))
            .ToList();
    }

    /// <summary>Возвращает топ-5 самых популярных тренеров по количеству тренировок</summary>
    public async Task<IList<TrainerDto>> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Аналитика: расчет топ-5 популярных тренеров");
        var topIds = await sessionRepo.GetTopFiveTrainerIds();
        var trainers = await trainerRepo.GetByIds(topIds);
        return topIds
            .Select(id => trainers.First(t => t.Id == id))
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }
}