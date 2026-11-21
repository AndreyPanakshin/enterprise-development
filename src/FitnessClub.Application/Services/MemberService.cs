using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис управления клиентами (Member) клуба
/// </summary>
public class MemberService(
    IMemberRepository memberRepo,
    ITrainingSessionRepository sessionRepo,
    ILogger<MemberService> logger) : IMemberService
{
    /// <summary>Создает нового клиента</summary>
    public async Task<MemberDto> Create(MemberCreateUpdateDto dto)
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
        await memberRepo.Add(entity);
        logger.LogInformation("Клиент успешно добавлен с ID: {Id}", entity.Id);
        return MapToDto(entity);
    }

    /// <summary>Получает клиента по идентификатору</summary>
    public async Task<MemberDto?> Get(int id)
    {
        logger.LogInformation("Поиск клиента по ID: {Id}", id);
        var target = await memberRepo.Get(id);
        if (target is null)
        {
            logger.LogWarning("Клиент с ID {Id} не найден в базе", id);
            return null;
        }
        return MapToDto(target);
    }

    /// <summary>Получает список всех клиентов</summary>
    public async Task<IList<MemberDto>> GetAll()
    {
        logger.LogInformation("Выгрузка полного списка клиентов");
        var members = await memberRepo.GetAll();
        return members.Select(MapToDto).ToList();
    }

    /// <summary>Обновляет данные клиента</summary>
    public async Task<MemberDto> Update(MemberCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление данных клиента с ID: {Id}", id);
        var target = await memberRepo.Get(id)
            ?? throw new KeyNotFoundException($"Клиент с ID {id} не обнаружен.");
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
        await memberRepo.Update(target);
        logger.LogInformation("Данные клиента #{Id} обновлены", id);
        return MapToDto(target);
    }

    /// <summary>Удаляет клиента, если у него нет тренировок</summary>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Попытка удаления клиента с ID: {Id}", id);
        var target = await memberRepo.Get(id);
        if (target is null) return false;
        if (await sessionRepo.HasSessionsForMember(id))
        {
            logger.LogWarning("Удаление клиента #{Id} заблокировано: есть привязанные тренировки", id);
            return false;
        }
        return await memberRepo.Delete(id);
    }

    /// <summary>Возвращает персональные тренировки клиента</summary>
    public async Task<IReadOnlyList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int memberId)
    {
        logger.LogInformation("Выгрузка тренировок для клиента #{Id}", memberId);
        _ = await memberRepo.Get(memberId)
            ?? throw new KeyNotFoundException($"Клиент с ID {memberId} не обнаружен.");
        var sessions = await sessionRepo.GetByMember(memberId);
        return sessions
            .Select(s => new PersonalTrainingSessionDto(
                s.Id, s.Member.Id, s.Trainer.Id, s.DateTime, s.HallName, s.IsTrial))
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