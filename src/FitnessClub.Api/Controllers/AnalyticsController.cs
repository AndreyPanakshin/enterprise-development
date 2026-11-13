using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для аналитических выборок и бизнес-отчетов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService analyticsSvc,
    ILogger<AnalyticsController> logger)
    : ControllerBase
{
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetExperiencedTrainers()
    {
        try
        {
            logger.LogInformation("Запрос отчета: тренеры со стажем от 5 лет");
            var result = analyticsSvc.GetTrainersWithFiveOrMoreYearsOfExperience();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при формировании отчета по опытным тренерам");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("hall-availability")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<bool> VerifyHallAvailability(
        [FromQuery] string hallName,
        [FromQuery] DateTime checkTime)
    {
        try
        {
            logger.LogInformation("Проверка занятости зала '{Hall}' на время {Time}", hallName, checkTime);
            var available = analyticsSvc.CheckHallAvailability(hallName, checkTime);
            return Ok(available);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры для проверки доступности зала");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка проверки доступности зала '{Hall}'", hallName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ClientDto>> GetExpiredMemberships()
    {
        try
        {
            logger.LogInformation("Запрос списка клиентов с просроченными абонементами");
            var data = analyticsSvc.GetClientsWithExpiredSubscription();
            return Ok(data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка выгрузки клиентов с просрочкой");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("current-month-sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetCurrentMonthHallSessions(
        [FromQuery] string hallName)
    {
        try
        {
            logger.LogInformation("Выгрузка занятий текущего месяца для зала '{Hall}'", hallName);
            var sessions = analyticsSvc.GetSessionsForCurrentMonthInSelectedHall(hallName);
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка выгрузки занятий для зала '{Hall}'", hallName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("top-trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTopTrainersList()
    {
        try
        {
            logger.LogInformation("Запрос тоП-5 популярных тренеров");
            var topTrainers = analyticsSvc.GetTopFivePopularTrainers();
            return Ok(topTrainers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при формировании топ-5 тренеров");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}