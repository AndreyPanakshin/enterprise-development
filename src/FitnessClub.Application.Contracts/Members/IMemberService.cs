using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Members;

/// <summary>
/// Сервис для управления клиентами (Member) клуба
/// </summary>
public interface IMemberService : IApplicationService<MemberDto, MemberCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список персональных тренировок клиента по его идентификатору
    /// </summary>
    /// <param name="memberId">Идентификатор клиента</param>
    /// <returns>Список тренировок</returns>
    public IReadOnlyList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int memberId);
}