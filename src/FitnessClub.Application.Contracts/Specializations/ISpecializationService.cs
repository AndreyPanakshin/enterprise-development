using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.Specializations;

public interface ISpecializationService : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>
{
    IList<TrainerDto> GetTrainers(int id);
}