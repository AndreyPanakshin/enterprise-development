using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления персональными тренировками
/// </summary>
public class PersonalTrainingSessionService(
    IRepository<TrainingSession, int> sessionRepo,
    IRepository<Member, int> clientRepo,
    IRepository<Trainer, int> trainerRepo,
    ILogger<PersonalTrainingSessionService> logger)
    : IApplicationService<PersonalTrainingSessionDto, PersonalTrainingSessionCreateUpdateDto, int>
{
    public PersonalTrainingSessionDto Create(PersonalTrainingSessionCreateUpdateDto dto)
    {
        logger.LogInformation("Запись клиента на персональное занятие");
        var client = clientRepo.Get(dto.MemberId) ?? throw new KeyNotFoundException($"Клиент #{dto.MemberId} не найден.");
        var trainer = trainerRepo.Get(dto.TrainerId) ?? throw new KeyNotFoundException($"Тренер #{dto.TrainerId} не найден.");

        var entity = new TrainingSession
        {
            Id = 0,
            Member = client,
            Trainer = trainer,
            DateTime = dto.SessionDateTime,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial
        };

        sessionRepo.Add(entity);
        return MapToDto(entity);
    }

    public PersonalTrainingSessionDto? Get(int id)
    {
        logger.LogInformation("Запрос тренировки по ID: {Id}", id);
        var target = sessionRepo.Get(id);
        return target is null ? null : MapToDto(target);
    }

    public IList<PersonalTrainingSessionDto> GetAll()
    {
        logger.LogInformation("Выгрузка всех запланированных тренировок");
        return sessionRepo.GetAll().Select(MapToDto).ToList();
    }

    public PersonalTrainingSessionDto Update(PersonalTrainingSessionCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Изменение параметров тренировки ID: {Id}", id);
        var session = sessionRepo.Get(id) ?? throw new KeyNotFoundException($"Тренировка #{id} не найдена.");
        var client = clientRepo.Get(dto.MemberId) ?? throw new KeyNotFoundException($"Клиент #{dto.MemberId} не найден.");
        var trainer = trainerRepo.Get(dto.TrainerId) ?? throw new KeyNotFoundException($"Тренер #{dto.TrainerId} не найден.");

        session.Member = client;
        session.Trainer = trainer;
        session.DateTime = dto.SessionDateTime;
        session.HallName = dto.HallName;
        session.IsTrial = dto.IsTrial;

        sessionRepo.Update(session);
        return MapToDto(session);
    }

    public bool Delete(int id)
    {
        logger.LogInformation("Отмена/удаление тренировки ID: {Id}", id);
        return sessionRepo.Delete(id);
    }

    private static PersonalTrainingSessionDto MapToDto(TrainingSession s) => new(
        s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial);
}