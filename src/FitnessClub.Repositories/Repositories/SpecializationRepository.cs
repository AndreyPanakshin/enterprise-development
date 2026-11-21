using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для специализаций
/// </summary>
public class SpecializationRepository : IRepository<Specialization, int>
{
    private readonly List<Specialization> _specializations;

    public SpecializationRepository(DataSeed data) => _specializations = data.Specializations;

    /// <summary>Получает специализацию по идентификатору</summary>
    public Task<Specialization?> Get(int id) => Task.FromResult(_specializations.SingleOrDefault(s => s.Id == id));

    /// <summary>Получает все специализации</summary>
    public Task<IReadOnlyList<Specialization>> GetAll() => Task.FromResult<IReadOnlyList<Specialization>>(_specializations);

    /// <summary>Добавляет специализацию</summary>
    public Task Add(Specialization entity)
    {
        entity.Id = _specializations.Count == 0 ? 1 : _specializations.Max(s => s.Id) + 1;
        _specializations.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>Обновляет специализацию</summary>
    public Task Update(Specialization entity)
    {
        var idx = _specializations.FindIndex(s => s.Id == entity.Id);
        if (idx == -1)
            throw new KeyNotFoundException($"Специализация с идентификатором {entity.Id} не найдена.");
        _specializations[idx] = entity;
        return Task.CompletedTask;
    }

    /// <summary>Удаляет специализацию</summary>
    public async Task<bool> Delete(int id)
    {
        var target = await Get(id);
        return target is not null && _specializations.Remove(target);
    }
}