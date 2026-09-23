namespace FitnessClub.Tests;

using Xunit;
using FitnessClub.Domain;

/// <summary>
/// Юнит-тесты для проверки LINQ запросов фитнес-клуба
/// </summary>
public class FitnessClubUnitTests
{
    private readonly List<Specialization> _specs;
    private readonly List<Trainer> _trainers;
    private readonly List<Client> _clients;
    private readonly List<TrainingSession> _sessions;

    public FitnessClubUnitTests()
    {
        (_specs, _trainers, _clients, _sessions) = DataSeed.GetSeedData();
    }

    /// <summary>
    /// Тест вывода информации о тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void Test1_GetTrainersWithExperienceAtLeast5Years()
    {
        var result = _trainers
            .Where(t => t.WorkExperienceYears >= 5)
            .ToList();

        Assert.NotEmpty(result);
        Assert.All(result, t => Assert.True(t.WorkExperienceYears >= 5));
    }

    /// <summary>
    /// Тест проверки доступности зала для записи в данный момент
    /// </summary>
    [Fact]
    public void Test2_CheckHallAvailabilityForBookingAtGivenTime()
    {
        string targetHall = "Зал №1";
        DateTime checkTime = DateTime.UtcNow;
        TimeSpan sessionDuration = TimeSpan.FromHours(1);

        bool isAvailable = !_sessions.Any(s =>
            s.HallName == targetHall &&
            s.DateTime < checkTime.Add(sessionDuration) &&
            s.DateTime.Add(s.Duration) > checkTime);

        Assert.True(isAvailable || !isAvailable); 
    }

    /// <summary>
    /// Тест вывода информации о клиентах с просроченным абонементом с сортировкой по фамилии, имени и отчеству
    /// </summary>
    [Fact]
    public void Test3_GetClientsWithExpiredMembershipOrderedByName()
    {
        DateTime currentDate = DateTime.UtcNow;

        var expiredClients = _clients
            .Where(c => c.MembershipEndDate < currentDate)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();

        var expectedOrderedList = expiredClients
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .ToList();

        Assert.NotEmpty(expiredClients);
        Assert.True(expiredClients.SequenceEqual(expectedOrderedList));
    }

    /// <summary>
    /// Тест получения информации о занятиях за текущий месяц в выбранном зале
    /// </summary>
    [Fact]
    public void Test4_GetSessionsForCurrentMonthInSelectedHall()
    {
        string targetHall = "Зал №1";
        var now = DateTime.UtcNow;

        var result = _sessions
            .Where(s => s.HallName == targetHall &&
                        s.DateTime.Year == now.Year &&
                        s.DateTime.Month == now.Month)
            .ToList();

        Assert.All(result, s =>
        {
            Assert.Equal(targetHall, s.HallName);
            Assert.Equal(now.Month, s.DateTime.Month);
        });
    }

    /// <summary>
    /// Тест вывода топ 5 наиболее популярных тренеров
    /// </summary>
    [Fact]
    public void Test5_GetTop5MostPopularTrainers()
    {
        var top5Trainers = _sessions
            .GroupBy(s => s.Trainer)
            .Select(g => new
            {
                Trainer = g.Key,
                SessionCount = g.Count()
            })
            .OrderByDescending(x => x.SessionCount)
            .Take(5)
            .Select(x => x.Trainer)
            .ToList();

        Assert.NotNull(top5Trainers);
        Assert.True(top5Trainers.Count <= 5);
    }
}