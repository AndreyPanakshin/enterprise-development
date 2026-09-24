using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using Xunit;

namespace FitnessClub.Tests;

/// <summary>
/// Юнит-тесты для проверки LINQ запросов фитнес-клуба
/// </summary>
public class FitnessClubTests
{
    /// <summary>
    ///  Тест вывода информации о тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void GetTrainers_WorkExperienceFiveYearsOrMore_ReturnsCorrectTrainerIds()
    {
        var expectedTrainerIds = new[] { 1, 3, 4, 6, 8, 9, 10 };

        var actualTrainerIds = DataSeed.Trainers
            .Where(trainer => trainer.WorkExperienceYears >= 5)
            .Select(trainer => trainer.Id)
            .ToList();

        Assert.Equal(expectedTrainerIds, actualTrainerIds);
    }

    /// <summary>
    /// 2. Проверка занятости зала на определённое время.
    /// </summary>
    [Fact]
    public void CheckHallAvailability_OccupiedTimeSlot_ReturnsFalse()
    {
        var targetHall = "Зал A";
        var targetTime = DataSeed.TrainingSession[0].DateTime; 

        var isAvailable = !DataSeed.TrainingSession
            .Any(session => session.HallName == targetHall && session.DateTime == targetTime);

        Assert.False(isAvailable);
    }

    /// <summary>
    /// 3. Вывод клиентов с просроченным абонементом с отсортированным ФИО.
    /// </summary>
    [Fact]
    public void GetClientsWithExpiredMembership_OrderedByFullName_ReturnsMatchingClients()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Today);
        var expectedClientIds = new[] { 1, 2, 3 }; 

        var actualClientIds = DataSeed.Members
            .Where(client => client.MembershipEndDate < currentDate)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .Select(client => client.Id)
            .ToList();

        Assert.Equal(expectedClientIds, actualClientIds);
    }

    /// <summary>
    /// 4. Получение занятий за текущий месяц в выбранном зале.
    /// </summary>
    [Fact]
    public void GetSessionsForCurrentMonth_SelectedHall_ReturnsExpectedSessionIds()
    {
        var targetHall = "Зал A";
        var today = DateTime.Today;

        var expectedSessionIds = new[] { 1, 2, 4, 6, 9, 12 };

        var actualSessionIds = DataSeed.TrainingSession
            .Where(session => session.HallName == targetHall 
                           && session.DateTime.Year == today.Year 
                           && session.DateTime.Month == today.Month)
            .Select(session => session.Id)
            .ToList();

        Assert.Equal(expectedSessionIds, actualSessionIds);
    }

    /// <summary>
    /// 5. Вывод топ-5 самых популярных тренеров по количеству проведённых занятий.
    /// </summary>
    [Fact]
    public void GetTopFivePopularTrainers_ReturnsCorrectRankedTrainerIds()
    {
        var expectedTopTrainerIds = new[] { 3, 1, 4, 6, 8 }; 

        var actualTopTrainerIds = DataSeed.TrainingSession
            .GroupBy(session => session.Trainer.Id)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        Assert.Equal(expectedTopTrainerIds, actualTopTrainerIds);
    }
}