using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Members;

/// <summary>
/// Информация о клиенте фитнес-клуба
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Gender">Пол</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="MembershipStartDate">Дата начала абонемента</param>
/// <param name="MembershipEndDate">Дата окончания абонемента</param>
public record MemberDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate);