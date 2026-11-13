using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер управления клиентами фитнес-клуба
/// </summary>
public class ClientsController(
    IClientService clientSvc,
    ILogger<ClientsController> logger)
    : BaseEntityController<ClientDto, ClientCreateUpdateDto, int>(clientSvc, logger)
{
    [HttpGet("{id}/sessions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<PersonalTrainingSessionDto>> GetClientSessions(int id)
    {
        try
        {
            logger.LogInformation("Выгрузка персональных занятий для клиента #{Id}", id);
            var sessions = clientSvc.GetPersonalTrainingSessions(id);
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