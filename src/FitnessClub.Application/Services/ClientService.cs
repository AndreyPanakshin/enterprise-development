using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализация сервиса управления клиентами клуба
/// </summary>
public class ClientService(
    IRepository<Member, int> clientRepo,
    IRepository<TrainingSession, int> sessionRepo,
    ILogger<ClientService> logger) : IClientService
{
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        logger.LogInformation("Запуск создания нового клиента");
        if (dto.StartSub >= dto.EndSub)
        {
            throw new ArgumentException("Срок действия абонемента указан некорректно: дата начала позже или равна дате окончания.");
        }

        var entity = new Member
        {
            Id = 0,
            PassportNumber = dto.PassportNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            MembershipStartDate = dto.StartSub,
            MembershipEndDate = dto.EndSub
        };

        clientRepo.Add(entity);
        logger.LogInformation("Клиент успешно добавлен с ID: {Id}", entity.Id);

        return MapToDto(entity);
    }

    public ClientDto? Get(int id)
    {
        logger.LogInformation("Поиск клиента по ID: {Id}", id);
        var target = clientRepo.Get(id);
        if (target is null)
        {
            logger.LogWarning("Клиент с ID {Id} не найден в базе", id);
            return null;
        }
        return MapToDto(target);
    }

    public IList<ClientDto> GetAll()
    {
        logger.LogInformation("Выгрузка полного списка клиентов");
        return clientRepo.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    public ClientDto Update(ClientCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление данных клиента с ID: {Id}", id);
        var target = clientRepo.Get(id) ?? throw new KeyNotFoundException($"Клиент с ID {id} не обнаружен.");

        if (dto.StartSub >= dto.EndSub)
        {
            throw new ArgumentException("Неверные даты абонемента при обновлении.");
        }

        target.PassportNumber = dto.PassportNumber;
        target.LastName = dto.LastName;
        target.FirstName = dto.FirstName;
        target.Patronymic = dto.Patronymic;
        target.Gender = dto.Gender;
        target.DateOfBirth = dto.DateOfBirth;
        target.PhoneNumber = dto.PhoneNumber;
        target.MembershipStartDate = dto.StartSub;
        target.MembershipEndDate = dto.EndSub;

        clientRepo.Update(target);
        logger.LogInformation("Данные клиента #{Id} обновлены", id);

        return MapToDto(target);
    }

    public bool Delete(int id)
    {
        logger.LogInformation("Попытка удаления клиента с ID: {Id}", id);
        var target = clientRepo.Get(id);
        if (target is null) return false;

        var hasSessions = sessionRepo.GetAll().Any(s => s.Member.Id == id);
        if (hasSessions)
        {
            logger.LogWarning("Удаление клиента #{Id} заблокировано: есть привязанные тренировки", id);
            return false;
        }

        return clientRepo.Delete(id);
    }

   public IReadOnlyList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int clientId)
{
    return sessionRepo.GetAll()
        .Where(s => s.Member.Id == clientId)
        .Select(s => new PersonalTrainingSessionDto(
            s.Id,
            s.Member.Id,
            s.Trainer.Id,
            s.DateTime,
            s.HallName,
            s.IsTrial))
        .ToList();
}

    private static ClientDto MapToDto(Member c) => new(
        c.Id, c.PassportNumber, c.LastName, c.FirstName, c.Patronymic,
        c.Gender, c.DateOfBirth, c.PhoneNumber, c.MembershipStartDate, c.MembershipEndDate);
}