namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// Информация о персональной тренировке для отображения
/// </summary>
/// <param name="Id">Уникальный идентификатор тренировки</param>
/// <param name="MemberId">Идентификатор клиента</param>
/// <param name="TrainerId">Идентификатор тренера</param>
/// <param name="SessionDateTime">Дата и время проведения тренировки</param>
/// <param name="HallName">Название зала</param>
/// <param name="IsTrial">Признак пробной тренировки</param>
public record PersonalTrainingSessionDto(
    int Id,
    int MemberId,
    int TrainerId,
    DateTime SessionDateTime,
    string HallName,
    bool IsTrial);