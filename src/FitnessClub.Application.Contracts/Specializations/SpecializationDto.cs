namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// Информация о специализации тренера
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="Name">Название специализации</param>
public record SpecializationDto(
    int Id,
    string Name);