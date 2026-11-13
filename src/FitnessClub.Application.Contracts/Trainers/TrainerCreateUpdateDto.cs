using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// Данные для создания или обновления тренера
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Gender">Пол</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="ExperienceYears">Стаж работы в годах</param>
public record TrainerCreateUpdateDto(
    [Required][MaxLength(20)] string PassportNumber,
    [Required][MaxLength(50)] string LastName,
    [Required][MaxLength(50)] string FirstName,
    [MaxLength(50)] string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    [Range(1, int.MaxValue)] int SpecializationId,
    [Range(0, 100)] int ExperienceYears);