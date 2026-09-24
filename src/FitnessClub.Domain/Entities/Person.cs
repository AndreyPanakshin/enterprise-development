using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Базовый класс человека
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный ID
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество 
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Полное имя 
    /// </summary>
    public string FullName => string.IsNullOrWhiteSpace(Patronymic)
        ? $"{LastName} {FirstName}"
        : $"{LastName} {FirstName} {Patronymic}";

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly DateOfBirth { get; set; }
}