using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Members;

/// <summary>
/// Данные для создания или обновления клиента
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Gender">Пол</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="StartSub">Дата начала абонемента</param>
/// <param name="EndSub">Дата окончания абонемента</param>
public record MemberCreateUpdateDto(
    [Required][MaxLength(20)] string PassportNumber,
    [Required][MaxLength(50)] string LastName,
    [Required][MaxLength(50)] string FirstName,
    [MaxLength(50)] string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    [Required][Phone] string PhoneNumber,
    DateOnly StartSub,
    DateOnly EndSub);