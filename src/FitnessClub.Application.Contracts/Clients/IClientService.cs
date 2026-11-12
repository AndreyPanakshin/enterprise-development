using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Clients;

/// <summary>
/// Сервис управления клиентами клуба
/// </summary>
public interface IClientService : IApplicationService<ClientDto, ClientCreateUpdateDto, int>
{
    public IReadOnlyList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int clientId);
}