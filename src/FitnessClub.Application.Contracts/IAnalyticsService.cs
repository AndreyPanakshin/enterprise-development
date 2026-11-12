using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts;

public interface IAnalyticsService
{
    IList<TrainerDto> GetTrainersWithFiveOrMoreYearsOfExperience();
    bool CheckHallAvailability(string hallName, DateTime checkTime);
    IList<ClientDto> GetClientsWithExpiredSubscription();
    IList<PersonalTrainingSessionDto> GetSessionsForCurrentMonthInSelectedHall(string hallName);
    IList<TrainerDto> GetTopFivePopularTrainers();
}