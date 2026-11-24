using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Специализация тренера
/// </summary>
[Table("specializations")]
public class Specialization
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }
}