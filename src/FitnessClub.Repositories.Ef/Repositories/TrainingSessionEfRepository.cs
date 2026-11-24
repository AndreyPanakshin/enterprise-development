using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Repositories.Ef.Repositories;

/// <summary>
/// Репозиторий персональных тренировок с хранением в MySQL
/// </summary>
public class TrainingSessionEfRepository(FitnessClubDbContext context) : ITrainingSessionRepository
{
    /// <summary>Получает тренировку по идентификатору</summary>
    public async Task<TrainingSession?> Get(int id)
    {
        return await context.TrainingSessions
            .Include(s => s.Member)
            .Include(s => s.Trainer)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>Получает все тренировки</summary>
    public async Task<IReadOnlyList<TrainingSession>> GetAll()
    {
        return await context.TrainingSessions
            .Include(s => s.Member)
            .Include(s => s.Trainer)
            .ToListAsync();
    }

    /// <summary>Добавляет тренировку</summary>
    public async Task Add(TrainingSession entity)
    {
        var member = await context.Members.FindAsync(entity.Member.Id)
            ?? throw new KeyNotFoundException("Клиент не найден.");
        var trainer = await context.Trainers.FindAsync(entity.Trainer.Id)
            ?? throw new KeyNotFoundException("Тренер не найден.");
        entity.Member = member;
        entity.Trainer = trainer;
        context.TrainingSessions.Add(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>Обновляет тренировку</summary>
    public async Task Update(TrainingSession entity)
    {
        var session = await context.TrainingSessions.FirstOrDefaultAsync(s => s.Id == entity.Id)
            ?? throw new KeyNotFoundException("Тренировка не найдена.");
        var member = await context.Members.FindAsync(entity.Member.Id)
            ?? throw new KeyNotFoundException("Клиент не найден.");
        var trainer = await context.Trainers.FindAsync(entity.Trainer.Id)
            ?? throw new KeyNotFoundException("Тренер не найден.");
        context.Entry(session).CurrentValues.SetValues(entity);
        session.Member = member;
        session.Trainer = trainer;
        await context.SaveChangesAsync();
    }

    /// <summary>Удаляет тренировку</summary>
    public async Task<bool> Delete(int id)
    {
        var session = await context.TrainingSessions.FindAsync(id);
        if (session is null) return false;
        context.TrainingSessions.Remove(session);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>Проверяет наличие тренировок у клиента</summary>
    public Task<bool> HasSessionsForMember(int memberId)
    {
        return context.TrainingSessions.AnyAsync(s => s.Member.Id == memberId);
    }

    /// <summary>Проверяет наличие тренировок у тренера</summary>
    public Task<bool> HasSessionsForTrainer(int trainerId)
    {
        return context.TrainingSessions.AnyAsync(s => s.Trainer.Id == trainerId);
    }

    /// <summary>Получает тренировки клиента</summary>
    public async Task<IReadOnlyList<TrainingSession>> GetByMember(int memberId)
    {
        return await context.TrainingSessions
            .Include(s => s.Member)
            .Include(s => s.Trainer)
            .AsNoTracking()
            .Where(s => s.Member.Id == memberId)
            .ToListAsync();
    }

    /// <summary>Получает тренировки тренера</summary>
    public async Task<IReadOnlyList<TrainingSession>> GetByTrainer(int trainerId)
    {
        return await context.TrainingSessions
            .Include(s => s.Member)
            .Include(s => s.Trainer)
            .AsNoTracking()
            .Where(s => s.Trainer.Id == trainerId)
            .ToListAsync();
    }

    /// <summary>Проверяет, зарегистрирован ли зал в системе</summary>
    public Task<bool> HallExists(string hallName)
    {
        return context.TrainingSessions.AnyAsync(s => s.HallName == hallName);
    }

    /// <summary>Проверяет занятость зала в указанное время</summary>
    public Task<bool> HasSessionInHall(string hallName, DateTime checkTime)
    {
        return context.TrainingSessions.AnyAsync(s => s.HallName == hallName && s.DateTime == checkTime);
    }

    /// <summary>Получает тренировки зала за указанный период</summary>
    public async Task<IReadOnlyList<TrainingSession>> GetByHallAndPeriod(string hallName, DateTime start, DateTime end)
    {
        return await context.TrainingSessions
            .Include(s => s.Member)
            .Include(s => s.Trainer)
            .AsNoTracking()
            .Where(s => s.HallName == hallName && s.DateTime >= start && s.DateTime < end)
            .ToListAsync();
    }

    /// <summary>Получает идентификаторы пяти самых популярных тренеров</summary>
    public async Task<IReadOnlyList<int>> GetTopFiveTrainerIds()
    {
        return await context.TrainingSessions
            .GroupBy(s => s.Trainer.Id)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.TrainerId)
            .Take(5)
            .Select(x => x.TrainerId)
            .ToListAsync();
    }
}