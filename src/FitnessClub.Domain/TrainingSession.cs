namespace FitnessClub.Domain;
/// <summary>
/// Запись клиента на тренировку
/// </summary>
public class TrainingSession
{
    /// <summary>
    /// Идентификатор занятия
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Идентификатор записанного клиента
    /// </summary>
    public required Guid ClientId { get; set; }
    /// <summary>
    /// Объект клиента
    /// </summary>
    public required Client Client { get; set; } = null!;
    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    public required Guid TrainerId { get; set; }
    /// <summary>
    /// Объект тренера
    /// </summary>
    public required Trainer Trainer { get; set; } = null!;
    /// <summary>
    /// Дата и время тренировки 
    /// </summary>
    public required DateTime DateTime { get; set; }
    /// <summary>
    /// Продолжительность тренировки
    /// </summary>
    public required TimeSpan Duration { get; set; } = TimeSpan.FromHours(1);
    /// <summary>
    /// Название зала
    /// </summary>
    public required string HallName { get; set; } = string.Empty;
    /// <summary>
    /// Признак пробного занятия
    /// </summary>
    public required bool IsTrial { get; set; }
}