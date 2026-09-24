namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы 
    /// </summary>
    public required int WorkExperienceYears { get; set; }
}