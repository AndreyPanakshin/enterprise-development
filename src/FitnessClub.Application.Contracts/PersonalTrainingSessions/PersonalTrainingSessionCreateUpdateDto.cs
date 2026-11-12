namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

public record PersonalTrainingSessionCreateUpdateDto(
    int ClientId,
    int TrainerId,
    DateTime SessionDateTime,
    string HallName,
    bool IsTrial);