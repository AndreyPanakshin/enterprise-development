using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления специализациями тренеров
/// </summary>
public class SpecializationService(
    IRepository<Specialization, int> specRepo,
    IRepository<Trainer, int> trainerRepo,
    ILogger<SpecializationService> logger) : ISpecializationService
{
    public SpecializationDto Create(SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Создание новой специализации: {Name}", dto.Name);
        var entity = new Specialization { Id = 0, Name = dto.Name };
        specRepo.Add(entity);
        return new SpecializationDto(entity.Id, entity.Name);
    }

    public SpecializationDto? Get(int id)
    {
        logger.LogInformation("Получение специализации по ID: {Id}", id);
        var item = specRepo.Get(id);
        return item is null ? null : new SpecializationDto(item.Id, item.Name);
    }

    public IList<SpecializationDto> GetAll()
    {
        logger.LogInformation("Выгрузка всех специализаций");
        return specRepo.GetAll().Select(s => new SpecializationDto(s.Id, s.Name)).ToList();
    }

    public SpecializationDto Update(SpecializationCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление специализации ID: {Id}", id);
        var item = specRepo.Get(id) ?? throw new KeyNotFoundException($"Специализация #{id} не найдена.");
        item.Name = dto.Name;
        specRepo.Update(item);
        return new SpecializationDto(item.Id, item.Name);
    }

    public bool Delete(int id)
    {
        logger.LogInformation("Удаление специализации ID: {Id}", id);
        var item = specRepo.Get(id);
        if (item is null) return false;

        if (trainerRepo.GetAll().Any(t => t.Specialization.Id == id))
        {
            logger.LogWarning("Удаление отклонено: специализация #{Id} назначена тренерам", id);
            return false;
        }

        return specRepo.Delete(id);
    }

    public IList<TrainerDto> GetTrainers(int id)
    {
        logger.LogInformation("Поиск тренеров по специализации ID: {Id}", id);
        _ = specRepo.Get(id) ?? throw new KeyNotFoundException($"Специализация #{id} не найдена.");

        return trainerRepo.GetAll()
            .Where(t => t.Specialization.Id == id)
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }
}