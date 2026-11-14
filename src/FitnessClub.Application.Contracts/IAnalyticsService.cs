using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts;

/// <summary>
/// Сервис для выполнения аналитических запросов и формирования бизнес-отчетов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>Возвращает тренеров со стажем работы 5 лет и более</summary>
    public IList<TrainerDto> GetTrainersWithFiveOrMoreYearsOfExperience();

    /// <summary>Проверяет, свободен ли указанный зал в заданное время</summary>
    public bool CheckHallAvailability(string hallName, DateTime checkTime);

    /// <summary>Возвращает клиентов с просроченными абонементами</summary>
    public IList<MemberDto> GetMembersWithExpiredSubscription();

    /// <summary>Возвращает тренировки в указанном зале за текущий месяц</summary>
    public IList<PersonalTrainingSessionDto> GetSessionsForCurrentMonthInSelectedHall(string hallName);

    /// <summary>Возвращает топ-5 самых популярных тренеров по количеству тренировок</summary>
    public IList<TrainerDto> GetTopFivePopularTrainers();
}