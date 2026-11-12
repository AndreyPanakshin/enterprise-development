using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис бизнес-логики для работы с тренерами
/// </summary>
public class TrainerService(
    IRepository<Trainer, int> trainerRepo,
    IRepository<Specialization, int> specRepo,
    IRepository<TrainingSession, int> sessionRepo,
    ILogger<TrainerService> logger) : ITrainerService
{
    public TrainerDto Create(TrainerCreateUpdateDto dto)
    {
        logger.LogInformation("Регистрация нового тренера");
        var spec = specRepo.Get(dto.SpecializationId) ?? throw new KeyNotFoundException($"Специализация #{dto.SpecializationId} не найдена.");

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

        trainerRepo.Add(entity);
        return MapToDto(entity);
    }

    public TrainerDto? Get(int id)
    {
        logger.LogInformation("Запрос тренера с ID: {Id}", id);
        var target = trainerRepo.Get(id);
        return target is null ? null : MapToDto(target);
    }

    public IList<TrainerDto> GetAll()
    {
        logger.LogInformation("Запрос списка всех тренеров клуба");
        return trainerRepo.GetAll().Select(MapToDto).ToList();
    }

    public TrainerDto Update(TrainerCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Изменение данных тренера ID: {Id}", id);
        var target = trainerRepo.Get(id) ?? throw new KeyNotFoundException($"Тренер #{id} не найден.");
        var spec = specRepo.Get(dto.SpecializationId) ?? throw new KeyNotFoundException($"Специализация #{dto.SpecializationId} не найдена.");

        target.PassportNumber = dto.PassportNumber;
        target.LastName = dto.LastName;
        target.FirstName = dto.FirstName;
        target.Patronymic = dto.Patronymic;
        target.Gender = dto.Gender;
        target.DateOfBirth = dto.DateOfBirth;
        target.Specialization = spec;
        target.WorkExperienceYears = dto.ExperienceYears;

        trainerRepo.Update(target);
        return MapToDto(target);
    }

    public bool Delete(int id)
    {
        logger.LogInformation("Удаление тренера ID: {Id}", id);
        var target = trainerRepo.Get(id);
        if (target is null) return false;

        if (sessionRepo.GetAll().Any(s => s.Trainer.Id == id))
        {
            logger.LogWarning("Невозможно удалить тренера #{Id}: есть активные записи на тренировки", id);
            return false;
        }

        return trainerRepo.Delete(id);
    }

    public IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение тренировок тренера ID: {Id}", id);
        _ = trainerRepo.Get(id) ?? throw new KeyNotFoundException($"Тренер #{id} не найден.");

        return sessionRepo.GetAll()
            .Where(s => s.Trainer.Id == id)
            .Select(s => new PersonalTrainingSessionDto(
                s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial))
            .ToList();
    }

    private static TrainerDto MapToDto(Trainer t) => new(
        t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
        t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears);
}