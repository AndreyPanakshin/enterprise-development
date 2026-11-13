using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для работы со специализациями тренеров
/// </summary>
public class SpecializationsController(
    ISpecializationService specSvc,
    ILogger<SpecializationsController> logger)
    : BaseEntityController<SpecializationDto, SpecializationCreateUpdateDto, int>(specSvc, logger)
{
    /// <summary>
    /// Возвращает список тренеров, имеющих указанную специализацию
    /// </summary>
    [HttpGet("{id}/trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTrainersBySpecialization(int id)
    {
        try
        {
            logger.LogInformation("Получение пула тренеров для специализации #{Id}", id);
            var list = specSvc.GetTrainers(id);
            return Ok(list);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Специализация с ID {Id} не найдена", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка получения тренеров по специализации #{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}