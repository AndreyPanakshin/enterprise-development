using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// Информация о тренере для отображения
/// </summary>
/// <param name="Id">Уникальный идентификатор тренера</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Gender">Пол</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="ExperienceYears">Стаж работы в годах</param>
public record TrainerDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    int SpecializationId,
    int ExperienceYears);