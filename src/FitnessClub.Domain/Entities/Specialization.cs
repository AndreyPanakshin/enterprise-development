namespace FitnessClub.Domain.Entities;

/// <summary>
/// Специализация тренера
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}