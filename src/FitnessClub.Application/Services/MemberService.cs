using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализация сервиса управления клиентами (Member) клуба
/// </summary>
public class MemberService(
    IRepository<Member, int> memberRepo,
    IRepository<TrainingSession, int> sessionRepo,
    ILogger<MemberService> logger) : IMemberService
{
    public MemberDto Create(MemberCreateUpdateDto dto)
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

        memberRepo.Add(entity);
        logger.LogInformation("Клиент успешно добавлен с ID: {Id}", entity.Id);
        return MapToDto(entity);
    }

    public MemberDto? Get(int id)
    {
        logger.LogInformation("Поиск клиента по ID: {Id}", id);
        var target = memberRepo.Get(id);
        if (target is null)
        {
            logger.LogWarning("Клиент с ID {Id} не найден в базе", id);
            return null;
        }
        return MapToDto(target);
    }

    public IList<MemberDto> GetAll()
    {
        logger.LogInformation("Выгрузка полного списка клиентов");
        return memberRepo.GetAll().Select(MapToDto).ToList();
    }

    public MemberDto Update(MemberCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление данных клиента с ID: {Id}", id);
        var target = memberRepo.Get(id) ?? throw new KeyNotFoundException($"Клиент с ID {id} не обнаружен.");
        
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

        memberRepo.Update(target);
        logger.LogInformation("Данные клиента #{Id} обновлены", id);
        return MapToDto(target);
    }

    public bool Delete(int id)
    {
        logger.LogInformation("Попытка удаления клиента с ID: {Id}", id);
        var target = memberRepo.Get(id);
        if (target is null) return false;

        var hasSessions = sessionRepo.GetAll().Any(s => s.Member.Id == id);
        if (hasSessions)
        {
            logger.LogWarning("Удаление клиента #{Id} заблокировано: есть привязанные тренировки", id);
            return false;
        }

        return memberRepo.Delete(id);
    }

    public IReadOnlyList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int memberId)
    {
        logger.LogInformation("Выгрузка тренировок для клиента #{Id}", memberId);
        return sessionRepo.GetAll()
            .Where(s => s.Member.Id == memberId)
            .Select(s => new PersonalTrainingSessionDto(
                s.Id,
                s.Member.Id,
                s.Trainer.Id,
                s.DateTime,
                s.HallName,
                s.IsTrial))
            .ToList();
    }

    private static MemberDto MapToDto(Member member) => new(
        member.Id, 
        member.PassportNumber, 
        member.LastName, 
        member.FirstName, 
        member.Patronymic,
        member.Gender, 
        member.DateOfBirth, 
        member.PhoneNumber, 
        member.MembershipStartDate, 
        member.MembershipEndDate);
}