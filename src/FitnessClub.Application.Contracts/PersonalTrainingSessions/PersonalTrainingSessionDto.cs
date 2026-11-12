namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// Уникальный DTO персонального занятия
/// </summary>
public record PersonalTrainingSessionDto(
    int Id,
    int ClientId,
    int TrainerId,
    DateTime SessionDateTime,  // Изменено название для уникальности
    string HallName,
    bool IsTrial);