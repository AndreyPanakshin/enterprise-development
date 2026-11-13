using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// Данные для создания или обновления специализации
/// </summary>
/// <param name="Name">Название специализации</param>
public record SpecializationCreateUpdateDto(
    [Required][MaxLength(100)] string Name);