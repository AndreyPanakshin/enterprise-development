using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Репозиторий тренеров клуба
/// </summary>
public interface ITrainerRepository : IRepository<Trainer, int>
{
    /// <summary>Проверяет наличие тренеров указанной специализации</summary>
    public Task<bool> HasTrainersForSpecialization(int specializationId);

    /// <summary>Получает тренеров указанной специализации</summary>
    public Task<IReadOnlyList<Trainer>> GetBySpecialization(int specializationId);

    /// <summary>Получает тренеров со стажем не менее указанного</summary>
    public Task<IReadOnlyList<Trainer>> GetWithMinExperience(int minYears);

    /// <summary>Получает тренеров по списку идентификаторов</summary>
    public Task<IReadOnlyList<Trainer>> GetByIds(IReadOnlyCollection<int> ids);
}