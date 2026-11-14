using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для клиентов
/// </summary>
public class MemberRepository : IRepository<Member, int>
{
    private readonly List<Member> _members;

    public MemberRepository(DataSeed data) => _members = data.Members;

    public Member? Get(int id) => _members.SingleOrDefault(m => m.Id == id);

    public IReadOnlyList<Member> GetAll() => _members;

    public void Add(Member entity)
    {
        entity.Id = _members.Count == 0 ? 1 : _members.Max(m => m.Id) + 1;
        _members.Add(entity);
    }

    public void Update(Member entity)
    {
        var idx = _members.FindIndex(m => m.Id == entity.Id);
        if (idx == -1)
            throw new KeyNotFoundException($"Клиент с идентификатором {entity.Id} не найден.");
        _members[idx] = entity;
    }

    public bool Delete(int id)
    {
        var target = Get(id);
        return target is not null && _members.Remove(target);
    }
}