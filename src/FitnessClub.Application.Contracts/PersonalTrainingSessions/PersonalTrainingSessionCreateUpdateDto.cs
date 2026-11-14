using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// Данные для создания или обновления персональной тренировки
/// </summary>
/// <param name="MemberId">Идентификатор клиента</param>
/// <param name="TrainerId">Идентификатор тренера</param>
/// <param name="SessionDateTime">Дата и время проведения тренировки</param>
/// <param name="HallName">Название зала</param>
/// <param name="IsTrial">Признак пробной тренировки</param>
public record PersonalTrainingSessionCreateUpdateDto(
    [Range(1, int.MaxValue)] int MemberId,
    [Range(1, int.MaxValue)] int TrainerId,
    DateTime SessionDateTime,
    [Required][MaxLength(100)] string HallName,
    bool IsTrial);