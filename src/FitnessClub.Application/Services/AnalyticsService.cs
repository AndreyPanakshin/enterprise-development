using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис выполнения аналитических бизнес-запросов
/// </summary>
public class AnalyticsService(
    IRepository<Trainer, int> trainerRepo,
    IRepository<Member, int> memberRepo,
    IRepository<TrainingSession, int> sessionRepo,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    public IList<TrainerDto> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Аналитика: фильтрация тренеров со стажем от 5 лет");
        return trainerRepo.GetAll()
            .Where(t => t.WorkExperienceYears >= 5)
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }

    public bool CheckHallAvailability(string hallName, DateTime checkTime)
    {
        logger.LogInformation("Аналитика: проверка доступности зала '{Hall}' на время {Time}", hallName, checkTime);
        var allSessions = sessionRepo.GetAll();
        return !allSessions.Any(s => s.HallName.Equals(hallName, StringComparison.OrdinalIgnoreCase) && s.DateTime == checkTime);
    }

    public IList<MemberDto> GetClientsWithExpiredSubscription()
    {
        logger.LogInformation("Аналитика: выгрузка просроченных абонементов");
        var currentDate = DateOnly.FromDateTime(DateTime.Today);
        return memberRepo.GetAll()
            .Where(c => c.IsMembershipExpired(currentDate))
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .Select(c => new MemberDto(
                c.Id, c.PassportNumber, c.LastName, c.FirstName, c.Patronymic,
                c.Gender, c.DateOfBirth, c.PhoneNumber, c.MembershipStartDate, c.MembershipEndDate))
            .ToList();
    }

    public IList<PersonalTrainingSessionDto> GetSessionsForCurrentMonthInSelectedHall(string hallName)
    {
        logger.LogInformation("Аналитика: занятия текущего месяца в зале '{Hall}'", hallName);
        var now = DateTime.Today;
        return sessionRepo.GetAll()
            .Where(s => s.HallName.Equals(hallName, StringComparison.OrdinalIgnoreCase) &&
                        s.DateTime.Year == now.Year &&
                        s.DateTime.Month == now.Month)
            .Select(s => new PersonalTrainingSessionDto(
                s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial))
            .ToList();
    }

    public IList<TrainerDto> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Аналитика: расчет топ-5 популярных тренеров");
        var topIds = sessionRepo.GetAll()
            .GroupBy(s => s.Trainer.Id)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();
            
        var cacheTrainers = trainerRepo.GetAll();
        return topIds
            .Select(id => cacheTrainers.First(t => t.Id == id))
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }
}