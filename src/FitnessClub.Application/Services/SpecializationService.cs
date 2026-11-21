using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления специализациями тренеров
/// </summary>
public class SpecializationService(
    IRepository<Specialization, int> specRepo,
    ITrainerRepository trainerRepo,
    ILogger<SpecializationService> logger) : ISpecializationService
{
    /// <summary>Создает новую специализацию</summary>
    public async Task<SpecializationDto> Create(SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Создание новой специализации: {Name}", dto.Name);
        var entity = new Specialization { Id = 0, Name = dto.Name };
        await specRepo.Add(entity);
        return new SpecializationDto(entity.Id, entity.Name);
    }

    /// <summary>Получает специализацию по идентификатору</summary>
    public async Task<SpecializationDto?> Get(int id)
    {
        logger.LogInformation("Получение специализации по ID: {Id}", id);
        var item = await specRepo.Get(id);
        return item is null ? null : new SpecializationDto(item.Id, item.Name);
    }

    /// <summary>Получает список всех специализаций</summary>
    public async Task<IList<SpecializationDto>> GetAll()
    {
        logger.LogInformation("Выгрузка всех специализаций");
        var items = await specRepo.GetAll();
        return items.Select(s => new SpecializationDto(s.Id, s.Name)).ToList();
    }

    /// <summary>Обновляет специализацию</summary>
    public async Task<SpecializationDto> Update(SpecializationCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление специализации ID: {Id}", id);
        var item = await specRepo.Get(id)
            ?? throw new KeyNotFoundException($"Специализация #{id} не найдена.");
        item.Name = dto.Name;
        await specRepo.Update(item);
        return new SpecializationDto(item.Id, item.Name);
    }

    /// <summary>Удаляет специализацию, если она не назначена тренерам</summary>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление специализации ID: {Id}", id);
        var item = await specRepo.Get(id);
        if (item is null) return false;
        if (await trainerRepo.HasTrainersForSpecialization(id))
        {
            logger.LogWarning("Удаление отклонено: специализация #{Id} назначена тренерам", id);
            return false;
        }
        return await specRepo.Delete(id);
    }

    /// <summary>Возвращает тренеров указанной специализации</summary>
    public async Task<IList<TrainerDto>> GetTrainers(int id)
    {
        logger.LogInformation("Поиск тренеров по специализации ID: {Id}", id);
        _ = await specRepo.Get(id)
            ?? throw new KeyNotFoundException($"Специализация #{id} не найдена.");
        var trainers = await trainerRepo.GetBySpecialization(id);
        return trainers
            .Select(t => new TrainerDto(
                t.Id, t.PassportNumber, t.LastName, t.FirstName, t.Patronymic,
                t.Gender, t.DateOfBirth, t.Specialization.Id, t.WorkExperienceYears))
            .ToList();
    }
}