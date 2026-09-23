namespace FitnessClub.Domain;
/// <summary>
/// Специализация тренера
/// </summary>
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; } = string.Empty;
}