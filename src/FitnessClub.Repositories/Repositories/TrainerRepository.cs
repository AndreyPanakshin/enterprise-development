using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для тренеров
/// </summary>
public class TrainerRepository : IRepository<Trainer, int>
{
    private readonly List<Trainer> _trainers;

    public TrainerRepository(DataSeed data)
    {
        _trainers = data.Trainers;
    }

    public Trainer? Get(int id)
    {
        return _trainers.SingleOrDefault(t => t.Id == id);
    }

    public IReadOnlyList<Trainer> GetAll()
    {
        return _trainers;
    }

    public void Add(Trainer entity)
    {
        entity.Id = _trainers.Count == 0 ? 1 : _trainers.Max(item => item.Id) + 1;
        _trainers.Add(entity);
    }

    public void Update(Trainer entity)
    {
        var idx = _trainers.FindIndex(t => t.Id == entity.Id);
        if (idx == -1)
        {
            throw new KeyNotFoundException($"Запись тренера с идентификатором {entity.Id} не обнаружена.");
        }
        _trainers[idx] = entity;
    }

    public bool Delete(int id)
    {
        var target = Get(id);
        return target is not null && _trainers.Remove(target);
    }
}