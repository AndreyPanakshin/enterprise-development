using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для персональных тренировок
/// </summary>
public class TrainingSessionRepository : IRepository<TrainingSession, int>
{
    private readonly List<TrainingSession> _sessions;

    public TrainingSessionRepository(DataSeed data)
    {
        _sessions = data.TrainingSessions;
    }

    public TrainingSession? Get(int id)
    {
        return _sessions.SingleOrDefault(p => p.Id == id);
    }

    public IReadOnlyList<TrainingSession> GetAll()
    {
        return _sessions;
    }

    public void Add(TrainingSession entity)
    {
        entity.Id = _sessions.Count == 0 ? 1 : _sessions.Max(item => item.Id) + 1;
        _sessions.Add(entity);
    }

    public void Update(TrainingSession entity)
    {
        var idx = _sessions.FindIndex(p => p.Id == entity.Id);
        if (idx == -1)
        {
            throw new KeyNotFoundException($"Персональная тренировка с идентификатором {entity.Id} не найдена.");
        }
        _sessions[idx] = entity;
    }

    public bool Delete(int id)
    {
        var target = Get(id);
        return target is not null && _sessions.Remove(target);
    }
}