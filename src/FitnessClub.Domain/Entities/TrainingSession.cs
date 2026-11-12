namespace FitnessClub.Domain.Entities;

/// <summary>
/// Запись клиента на тренировку
/// </summary>
public class TrainingSession
{
    /// <summary>
    /// Идентификатор занятия
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Объект клиента
    /// </summary>
    public required Member Member { get; set; }

    /// <summary>
    /// Объект тренера
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Дата и время тренировки 
    /// </summary>
    public required DateTime DateTime { get; set; }

    /// <summary>
    /// Продолжительность тренировки
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Название зала
    /// </summary>
    public required string HallName { get; set; }

    /// <summary>
    /// Признак пробного занятия
    /// </summary>
    public required bool IsTrial { get; set; }
}