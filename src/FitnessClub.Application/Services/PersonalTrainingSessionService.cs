using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления персональными тренировками
/// </summary>
public class PersonalTrainingSessionService(
    ITrainingSessionRepository sessionRepo,
    IMemberRepository memberRepo,
    ITrainerRepository trainerRepo,
    ILogger<PersonalTrainingSessionService> logger)
    : IApplicationService<PersonalTrainingSessionDto, PersonalTrainingSessionCreateUpdateDto, int>
{
    /// <summary>Создает запись на персональную тренировку</summary>
    public async Task<PersonalTrainingSessionDto> Create(PersonalTrainingSessionCreateUpdateDto dto)
    {
        logger.LogInformation("Запись клиента на персональное занятие");
        var member = await memberRepo.Get(dto.MemberId)
            ?? throw new KeyNotFoundException($"Клиент #{dto.MemberId} не найден.");
        var trainer = await trainerRepo.Get(dto.TrainerId)
            ?? throw new KeyNotFoundException($"Тренер #{dto.TrainerId} не найден.");
        var entity = new TrainingSession
        {
            Id = 0,
            Member = member,
            Trainer = trainer,
            DateTime = dto.SessionDateTime,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial
        };
        await sessionRepo.Add(entity);
        return MapToDto(entity);
    }

    /// <summary>Получает тренировку по идентификатору</summary>
    public async Task<PersonalTrainingSessionDto?> Get(int id)
    {
        logger.LogInformation("Запрос тренировки по ID: {Id}", id);
        var target = await sessionRepo.Get(id);
        return target is null ? null : MapToDto(target);
    }

    /// <summary>Получает список всех тренировок</summary>
    public async Task<IList<PersonalTrainingSessionDto>> GetAll()
    {
        logger.LogInformation("Выгрузка всех запланированных тренировок");
        var sessions = await sessionRepo.GetAll();
        return sessions.Select(MapToDto).ToList();
    }

    /// <summary>Обновляет параметры тренировки</summary>
    public async Task<PersonalTrainingSessionDto> Update(PersonalTrainingSessionCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Изменение параметров тренировки ID: {Id}", id);
        var session = await sessionRepo.Get(id)
            ?? throw new KeyNotFoundException($"Тренировка #{id} не найдена.");
        var member = await memberRepo.Get(dto.MemberId)
            ?? throw new KeyNotFoundException($"Клиент #{dto.MemberId} не найден.");
        var trainer = await trainerRepo.Get(dto.TrainerId)
            ?? throw new KeyNotFoundException($"Тренер #{dto.TrainerId} не найден.");
        session.Member = member;
        session.Trainer = trainer;
        session.DateTime = dto.SessionDateTime;
        session.HallName = dto.HallName;
        session.IsTrial = dto.IsTrial;
        await sessionRepo.Update(session);
        return MapToDto(session);
    }

    /// <summary>Удаляет тренировку</summary>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Отмена/удаление тренировки ID: {Id}", id);
        return await sessionRepo.Delete(id);
    }

    private static PersonalTrainingSessionDto MapToDto(TrainingSession s) => new(
        s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial);
}