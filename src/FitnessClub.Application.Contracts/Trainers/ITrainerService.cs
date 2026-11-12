using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Trainers;

public interface ITrainerService : IApplicationService<TrainerDto, TrainerCreateUpdateDto, int>
{
    IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id);
}