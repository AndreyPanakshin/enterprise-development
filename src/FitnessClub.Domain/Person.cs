namespace FitnessClub.Domain;

/// <summary>
/// Базовый класс человека
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный ID
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество (при наличии)
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