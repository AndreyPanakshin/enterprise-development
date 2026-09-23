namespace FitnessClub.Domain;

/// <summary>
/// Тренер
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public required Guid SpecializationId { get; set; }
    /// <summary>
    /// Специализация тренера
    /// </summary>
    public required Specialization Specialization { get; set; } = null!;
    /// <summary>
    /// Стаж работы 
    /// </summary>
    public required int WorkExperienceYears { get; set; }
}