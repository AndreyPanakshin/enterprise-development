using FitnessClub.Application.Contracts.Members;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами (Member) фитнес-клуба
/// </summary>
public class MembersController(
    IMemberService memberSvc,
    ILogger<MembersController> logger)
    : BaseEntityController<MemberDto, MemberCreateUpdateDto, int>(memberSvc, logger)
{
    /// <summary>
    /// Возвращает список персональных тренировок конкретного клиента
    /// </summary>
    [HttpGet("{id}/sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetMemberSessions(int id)
    {
        try
        {
            logger.LogInformation("Выгрузка персональных занятий для клиента #{Id}", id);
            var sessions = memberSvc.GetPersonalTrainingSessions(id);
            return Ok(sessions);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Клиент с ID {Id} не найден при запросе занятий", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении занятий клиента #{Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}