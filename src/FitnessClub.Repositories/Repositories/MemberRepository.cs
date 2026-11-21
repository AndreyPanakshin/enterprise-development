using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Repositories.Repositories;

/// <summary>
/// In-Memory хранилище для клиентов
/// </summary>
public class MemberRepository : IMemberRepository
{
    private readonly List<Member> _members;

    public MemberRepository(DataSeed data) => _members = data.Members;

    /// <summary>Получает клиента по идентификатору</summary>
    public Task<Member?> Get(int id) => Task.FromResult(_members.SingleOrDefault(m => m.Id == id));

    /// <summary>Получает всех клиентов</summary>
    public Task<IReadOnlyList<Member>> GetAll() => Task.FromResult<IReadOnlyList<Member>>(_members);

    /// <summary>Добавляет клиента</summary>
    public Task Add(Member entity)
    {
        entity.Id = _members.Count == 0 ? 1 : _members.Max(m => m.Id) + 1;
        _members.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>Обновляет клиента</summary>
    public Task Update(Member entity)
    {
        var idx = _members.FindIndex(m => m.Id == entity.Id);
        if (idx == -1)
            throw new KeyNotFoundException($"Клиент с идентификатором {entity.Id} не найден.");
        _members[idx] = entity;
        return Task.CompletedTask;
    }

    /// <summary>Удаляет клиента</summary>
    public async Task<bool> Delete(int id)
    {
        var target = await Get(id);
        return target is not null && _members.Remove(target);
    }

    /// <summary>Получает клиентов с просроченными абонементами, отсортированных по ФИО</summary>
    public async Task<IReadOnlyList<Member>> GetExpiredSubscriptions(DateOnly today)
    {
        return (await GetAll())
            .Where(m => m.IsMembershipExpired(today))
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ThenBy(m => m.Patronymic)
            .ToList();
    }
}