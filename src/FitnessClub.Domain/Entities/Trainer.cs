using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер
/// </summary>
[Table("trainers")]
public class Trainer : Person
{
    /// <summary>
    /// Специализация тренера
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    [Column("work_experience_years")]
    public required int WorkExperienceYears { get; set; }
}