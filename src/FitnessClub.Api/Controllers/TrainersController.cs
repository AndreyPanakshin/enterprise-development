using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления тренерами клуба
/// </summary>
public class TrainersController(
    ITrainerService trainerSvc,
    ILogger<TrainersController> logger)
    : BaseEntityController<TrainerDto, TrainerCreateUpdateDto, int>(trainerSvc, logger)
{
    /// <summary>
    /// Возвращает список персональных тренировок конкретного тренера
    /// </summary>
    [HttpGet("{id}/sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetTrainerSessions(int id)
    {
        try
        {
            logger.LogInformation("Выгрузка занятий тренера #{Id}", id);
            var data = trainerSvc.GetPersonalTrainingSessions(id);
            return Ok(data);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Тренер с ID {Id} не найден", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка выгрузки занятий тренера #{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}