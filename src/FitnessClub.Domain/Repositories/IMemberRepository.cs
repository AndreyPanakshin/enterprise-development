using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Репозиторий клиентов клуба
/// </summary>
public interface IMemberRepository : IRepository<Member, int>
{
    /// <summary>Получает клиентов с просроченными абонементами, отсортированных по ФИО</summary>
    public Task<IReadOnlyList<Member>> GetExpiredSubscriptions(DateOnly today);
}