using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для тренеров
/// </summary>
public class TrainerRepository : ITrainerRepository
{
    private readonly List<Trainer> _trainers;

    public TrainerRepository(DataSeed data) => _trainers = data.Trainers;

    /// <summary>Получает тренера по идентификатору</summary>
    public Task<Trainer?> Get(int id) => Task.FromResult(_trainers.SingleOrDefault(t => t.Id == id));

    /// <summary>Получает всех тренеров</summary>
    public Task<IReadOnlyList<Trainer>> GetAll() => Task.FromResult<IReadOnlyList<Trainer>>(_trainers);

    /// <summary>Добавляет тренера</summary>
    public Task Add(Trainer entity)
    {
        entity.Id = _trainers.Count == 0 ? 1 : _trainers.Max(t => t.Id) + 1;
        _trainers.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>Обновляет тренера</summary>
    public Task Update(Trainer entity)
    {
        var idx = _trainers.FindIndex(t => t.Id == entity.Id);
        if (idx == -1)
            throw new KeyNotFoundException($"Запись тренера с идентификатором {entity.Id} не обнаружена.");
        _trainers[idx] = entity;
        return Task.CompletedTask;
    }

    /// <summary>Удаляет тренера</summary>
    public async Task<bool> Delete(int id)
    {
        var target = await Get(id);
        return target is not null && _trainers.Remove(target);
    }

    /// <summary>Проверяет наличие тренеров указанной специализации</summary>
    public Task<bool> HasTrainersForSpecialization(int specializationId)
    {
        return Task.FromResult(_trainers.AsQueryable().Any(t => t.Specialization.Id == specializationId));
    }

    /// <summary>Получает тренеров указанной специализации</summary>
    public Task<IReadOnlyList<Trainer>> GetBySpecialization(int specializationId)
    {
        IReadOnlyList<Trainer> result = _trainers.AsQueryable()
            .Where(t => t.Specialization.Id == specializationId)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>Получает тренеров со стажем не менее указанного</summary>
    public Task<IReadOnlyList<Trainer>> GetWithMinExperience(int minYears)
    {
        IReadOnlyList<Trainer> result = _trainers.AsQueryable()
            .Where(t => t.WorkExperienceYears >= minYears)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>Получает тренеров по списку идентификаторов</summary>
    public Task<IReadOnlyList<Trainer>> GetByIds(IReadOnlyCollection<int> ids)
    {
        IReadOnlyList<Trainer> result = _trainers.AsQueryable()
            .Where(t => ids.Contains(t.Id))
            .ToList();
        return Task.FromResult(result);
    }
}