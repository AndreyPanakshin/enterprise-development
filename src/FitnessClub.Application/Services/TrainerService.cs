using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис бизнес-логики для работы с тренерами
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepo,
    IRepository<Specialization, int> specRepo,
    ITrainingSessionRepository sessionRepo,
    ILogger<TrainerService> logger) : ITrainerService
{
    /// <summary>Создает нового тренера</summary>
    public async Task<TrainerDto> Create(TrainerCreateUpdateDto dto)
    {
        logger.LogInformation("Регистрация нового тренера");
        var spec = await specRepo.Get(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация #{dto.SpecializationId} не найдена.");
        var entity = new Trainer
        {
            Id = 0,
            PassportNumber = dto.PassportNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            Specialization = spec,
            WorkExperienceYears = dto.ExperienceYears
        };
        await trainerRepo.Add(entity);
        return MapToDto(entity);
    }

    /// <summary>Получает тренера по идентификатору</summary>
    public async Task<TrainerDto?> Get(int id)
    {
        logger.LogInformation("Запрос тренера с ID: {Id}", id);
        var target = await trainerRepo.Get(id);
        return target is null ? null : MapToDto(target);
    }

    /// <summary>Получает список всех тренеров</summary>
    public async Task<IList<TrainerDto>> GetAll()
    {
        logger.LogInformation("Запрос списка всех тренеров клуба");
        var trainers = await trainerRepo.GetAll();
        return trainers.Select(MapToDto).ToList();
    }

    /// <summary>Обновляет данные тренера</summary>
    public async Task<TrainerDto> Update(TrainerCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Изменение данных тренера ID: {Id}", id);
        var target = await trainerRepo.Get(id)
            ?? throw new KeyNotFoundException($"Тренер #{id} не найден.");
        var spec = await specRepo.Get(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация #{dto.SpecializationId} не найдена.");
        target.PassportNumber = dto.PassportNumber;
        target.LastName = dto.LastName;
        target.FirstName = dto.FirstName;
        target.Patronymic = dto.Patronymic;
        target.Gender = dto.Gender;
        target.DateOfBirth = dto.DateOfBirth;
        target.Specialization = spec;
        target.WorkExperienceYears = dto.ExperienceYears;
        await trainerRepo.Update(target);
        return MapToDto(target);
    }

    /// <summary>Удаляет тренера, если у него нет тренировок</summary>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление тренера ID: {Id}", id);
        var target = await trainerRepo.Get(id);
        if (target is null) return false;
        if (await sessionRepo.HasSessionsForTrainer(id))
        {
            logger.LogWarning("Невозможно удалить тренера #{Id}: есть активные записи на тренировки", id);
            return false;
        }
        return await trainerRepo.Delete(id);
    }

    /// <summary>Возвращает персональные тренировки тренера</summary>
    public async Task<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение тренировок тренера ID: {Id}", id);
        _ = await trainerRepo.Get(id)
            ?? throw new KeyNotFoundException($"Тренер #{id} не найден.");
        var sessions = await sessionRepo.GetByTrainer(id);
        return sessions
            .Select(s => new PersonalTrainingSessionDto(
                s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial))
            .ToList();
    }

    private static TrainerDto MapToDto(Trainer t) => new(
        t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
        t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears);
}