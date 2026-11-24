using System.ComponentModel.DataAnnotations.Schema;
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
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Column("passport_number")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество (при наличии)
    /// </summary>
    [Column("patronymic")]
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
    [Column("gender")]
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Column("date_of_birth")]
    public required DateOnly DateOfBirth { get; set; }
}