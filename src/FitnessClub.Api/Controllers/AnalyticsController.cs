using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для получения аналитических выборок и бизнес-отчетов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAnalyticsService analyticsSvc,
    ILogger<AnalyticsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает список тренеров со стажем работы 5 лет и более
    /// </summary>
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

    /// <summary>
    /// Проверяет доступность указанного зала на заданное время
    /// </summary>
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

    /// <summary>
    /// Возвращает список клиентов с просроченными абонементами
    /// </summary>
    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<MemberDto>> GetExpiredMemberships()
    {
        try
        {
            logger.LogInformation("Запрос списка клиентов с просроченными абонементами");
            var data = analyticsSvc.GetMembersWithExpiredSubscription();
            return Ok(data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка выгрузки клиентов с просрочкой");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Возвращает список персональных тренировок в указанном зале за текущий месяц
    /// </summary>
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
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры для выгрузки занятий зала");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка выгрузки занятий для зала '{Hall}'", hallName);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Возвращает список из 5 самых популярных тренеров
    /// </summary>
    [HttpGet("top-trainers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TrainerDto>> GetTopTrainersList()
    {
        try
        {
            logger.LogInformation("Запрос топ-5 популярных тренеров");
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