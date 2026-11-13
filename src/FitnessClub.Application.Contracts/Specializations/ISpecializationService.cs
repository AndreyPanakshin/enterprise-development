using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// Сервис для управления специализациями тренеров
/// </summary>
public interface ISpecializationService : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список тренеров с указанной специализацией
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Список тренеров</returns>
    public IList<TrainerDto> GetTrainers(int id);
}