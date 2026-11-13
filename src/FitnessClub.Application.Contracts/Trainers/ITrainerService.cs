using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// Сервис для управления тренерами фитнес-клуба
/// </summary>
public interface ITrainerService : IApplicationService<TrainerDto, TrainerCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список персональных тренировок конкретного тренера
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <returns>Список тренировок</returns>
    public IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id);
}