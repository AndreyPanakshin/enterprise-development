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

    public SpecializationRepository(DataSeed data)
    {
        _specializations = data.Specializations;
    }

    public Specialization? Get(int id)
    {
        return _specializations.SingleOrDefault(s => s.Id == id);
    }

    public IReadOnlyList<Specialization> GetAll()
    {
        return _specializations;
    }

    public void Add(Specialization entity)
    {
        entity.Id = _specializations.Count == 0 ? 1 : _specializations.Max(item => item.Id) + 1;
        _specializations.Add(entity);
    }

    public void Update(Specialization entity)
    {
        var idx = _specializations.FindIndex(s => s.Id == entity.Id);
        if (idx == -1)
        {
            throw new KeyNotFoundException($"Специализация с идентификатором {entity.Id} не найдена.");
        }
        _specializations[idx] = entity;
    }

    public bool Delete(int id)
    {
        var target = Get(id);
        return target is not null && _specializations.Remove(target);
    }
}