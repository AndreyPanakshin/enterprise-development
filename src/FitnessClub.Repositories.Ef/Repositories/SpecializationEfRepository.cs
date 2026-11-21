using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Repositories.Ef.Repositories;

/// <summary>
/// Репозиторий специализаций с хранением в MySQL
/// </summary>
public class SpecializationEfRepository(FitnessClubDbContext context) : IRepository<Specialization, int>
{
    /// <summary>Получает специализацию по идентификатору</summary>
    public async Task<Specialization?> Get(int id)
    {
        return await context.Specializations.FindAsync(id);
    }

    /// <summary>Получает все специализации</summary>
    public async Task<IReadOnlyList<Specialization>> GetAll()
    {
        return await context.Specializations.ToListAsync();
    }

    /// <summary>Добавляет специализацию</summary>
    public async Task Add(Specialization entity)
    {
        context.Specializations.Add(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>Обновляет специализацию</summary>
    public async Task Update(Specialization entity)
    {
        context.Specializations.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>Удаляет специализацию</summary>
    public async Task<bool> Delete(int id)
    {
        var specialization = await context.Specializations.FindAsync(id);
        if (specialization is null) return false;
        context.Specializations.Remove(specialization);
        await context.SaveChangesAsync();
        return true;
    }
}