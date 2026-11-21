using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Репозиторий персональных тренировок
/// </summary>
public interface ITrainingSessionRepository : IRepository<TrainingSession, int>
{
    /// <summary>Проверяет наличие тренировок у клиента</summary>
    public Task<bool> HasSessionsForMember(int memberId);

    /// <summary>Проверяет наличие тренировок у тренера</summary>
    public Task<bool> HasSessionsForTrainer(int trainerId);

    /// <summary>Получает тренировки клиента</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByMember(int memberId);

    /// <summary>Получает тренировки тренера</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByTrainer(int trainerId);

    /// <summary>Проверяет, зарегистрирован ли зал в системе</summary>
    public Task<bool> HallExists(string hallName);

    /// <summary>Проверяет занятость зала в указанное время</summary>
    public Task<bool> HasSessionInHall(string hallName, DateTime checkTime);

    /// <summary>Получает тренировки зала за указанный период</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByHallAndPeriod(string hallName, DateTime start, DateTime end);

    /// <summary>Получает идентификаторы пяти самых популярных тренеров</summary>
    public Task<IReadOnlyList<int>> GetTopFiveTrainerIds();
}