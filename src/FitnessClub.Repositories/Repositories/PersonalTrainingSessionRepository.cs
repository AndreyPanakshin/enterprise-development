using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для персональных тренировок
/// </summary>
public class TrainingSessionRepository : ITrainingSessionRepository
{
    private readonly List<TrainingSession> _sessions;

    public TrainingSessionRepository(DataSeed data) => _sessions = data.TrainingSessions;

    /// <summary>Получает тренировку по идентификатору</summary>
    public Task<TrainingSession?> Get(int id) => Task.FromResult(_sessions.SingleOrDefault(s => s.Id == id));

    /// <summary>Получает все тренировки</summary>
    public Task<IReadOnlyList<TrainingSession>> GetAll() => Task.FromResult<IReadOnlyList<TrainingSession>>(_sessions);

    /// <summary>Добавляет тренировку</summary>
    public Task Add(TrainingSession entity)
    {
        entity.Id = _sessions.Count == 0 ? 1 : _sessions.Max(s => s.Id) + 1;
        _sessions.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>Обновляет тренировку</summary>
    public Task Update(TrainingSession entity)
    {
        var idx = _sessions.FindIndex(s => s.Id == entity.Id);
        if (idx == -1)
            throw new KeyNotFoundException($"Персональная тренировка с идентификатором {entity.Id} не найдена.");
        _sessions[idx] = entity;
        return Task.CompletedTask;
    }

    /// <summary>Удаляет тренировку</summary>
    public async Task<bool> Delete(int id)
    {
        var target = await Get(id);
        return target is not null && _sessions.Remove(target);
    }

    /// <summary>Проверяет наличие тренировок у клиента</summary>
    public Task<bool> HasSessionsForMember(int memberId)
    {
        return Task.FromResult(_sessions.AsQueryable().Any(s => s.Member.Id == memberId));
    }

    /// <summary>Проверяет наличие тренировок у тренера</summary>
    public Task<bool> HasSessionsForTrainer(int trainerId)
    {
        return Task.FromResult(_sessions.AsQueryable().Any(s => s.Trainer.Id == trainerId));
    }

    /// <summary>Получает тренировки клиента</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByMember(int memberId)
    {
        IReadOnlyList<TrainingSession> result = _sessions.AsQueryable()
            .Where(s => s.Member.Id == memberId)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>Получает тренировки тренера</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByTrainer(int trainerId)
    {
        IReadOnlyList<TrainingSession> result = _sessions.AsQueryable()
            .Where(s => s.Trainer.Id == trainerId)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>Проверяет, зарегистрирован ли зал в системе</summary>
    public Task<bool> HallExists(string hallName)
    {
        return Task.FromResult(_sessions.AsQueryable()
            .Any(s => s.HallName.Equals(hallName, StringComparison.OrdinalIgnoreCase)));
    }

    /// <summary>Проверяет занятость зала в указанное время</summary>
    public Task<bool> HasSessionInHall(string hallName, DateTime checkTime)
    {
        return Task.FromResult(_sessions.AsQueryable()
            .Any(s => s.HallName.Equals(hallName, StringComparison.OrdinalIgnoreCase) && s.DateTime == checkTime));
    }

    /// <summary>Получает тренировки зала за указанный период</summary>
    public Task<IReadOnlyList<TrainingSession>> GetByHallAndPeriod(string hallName, DateTime start, DateTime end)
    {
        IReadOnlyList<TrainingSession> result = _sessions.AsQueryable()
            .Where(s => s.HallName.Equals(hallName, StringComparison.OrdinalIgnoreCase)
                        && s.DateTime >= start && s.DateTime < end)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>Получает идентификаторы пяти самых популярных тренеров</summary>
    public Task<IReadOnlyList<int>> GetTopFiveTrainerIds()
    {
        IReadOnlyList<int> result = _sessions.AsQueryable()
            .GroupBy(s => s.Trainer.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key)
            .ToList();
        return Task.FromResult(result);
    }
}