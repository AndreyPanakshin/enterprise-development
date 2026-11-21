using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Repositories.Ef.Repositories;

/// <summary>
/// Репозиторий клиентов с хранением в MySQL
/// </summary>
public class MemberEfRepository(FitnessClubDbContext context) : IMemberRepository
{
    /// <summary>Получает клиента по идентификатору</summary>
    public async Task<Member?> Get(int id)
    {
        return await context.Members.FindAsync(id);
    }

    /// <summary>Получает всех клиентов</summary>
    public async Task<IReadOnlyList<Member>> GetAll()
    {
        return await context.Members.ToListAsync();
    }

    /// <summary>Добавляет клиента</summary>
    public async Task Add(Member entity)
    {
        context.Members.Add(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>Обновляет клиента</summary>
    public async Task Update(Member entity)
    {
        context.Members.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>Удаляет клиента</summary>
    public async Task<bool> Delete(int id)
    {
        var member = await context.Members.FindAsync(id);
        if (member is null) return false;
        context.Members.Remove(member);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>Получает клиентов с просроченными абонементами, отсортированных по ФИО</summary>
    public async Task<IReadOnlyList<Member>> GetExpiredSubscriptions(DateOnly today)
    {
        return await context.Members
            .AsNoTracking()
            .Where(m => m.MembershipEndDate < today)
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ThenBy(m => m.Patronymic)
            .ToListAsync();
    }
}