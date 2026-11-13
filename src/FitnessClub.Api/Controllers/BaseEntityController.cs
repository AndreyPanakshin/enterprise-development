using FitnessClub.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Базовый контроллер для выполнения стандартных CRUD-операций над сущностями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseEntityController<TModel, TDataDto, TIdentifier>(
    IApplicationService<TModel, TDataDto, TIdentifier> appService,
    ILogger loggerInstance)
    : ControllerBase
    where TModel : class
    where TDataDto : class
    where TIdentifier : struct
{
    /// <summary>
    /// Получает список всех записей данной сущности
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TModel>> GetAllItems()
    {
        try
        {
            loggerInstance.LogInformation("Запрос на получение всех записей сущности {ModelName}", typeof(TModel).Name);
            var collection = appService.GetAll();
            return Ok(collection);
        }
        catch (Exception ex)
        {
            loggerInstance.LogError(ex, "Сбой при выгрузке всех записей сущности {ModelName}", typeof(TModel).Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получает запись сущности по её уникальному идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TModel> GetItemById(TIdentifier id)
    {
        try
        {
            loggerInstance.LogInformation("Запрос записи {ModelName} по идентификатору {Id}", typeof(TModel).Name, id);
            var item = appService.Get(id);
            if (item is null)
            {
                loggerInstance.LogWarning("Запись {ModelName} с идентификатором {Id} не обнаружена", typeof(TModel).Name, id);
                return NotFound();
            }
            return Ok(item);
        }
        catch (Exception ex)
        {
            loggerInstance.LogError(ex, "Сбой при получении записи {ModelName} с идентификатором {Id}", typeof(TModel).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создает новую запись сущности на основе переданных данных
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TModel> CreateItem([FromBody] TDataDto payload)
    {
        try
        {
            loggerInstance.LogInformation("Создание новой записи для {ModelName}", typeof(TModel).Name);
            var created = appService.Create(payload);
            return StatusCode(StatusCodes.Status201Created, created);
        }
        catch (KeyNotFoundException ex)
        {
            loggerInstance.LogWarning(ex, "Связанная сущность не найдена при создании {ModelName}", typeof(TModel).Name);
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            loggerInstance.LogWarning(ex, "Неверные параметры при создании {ModelName}", typeof(TModel).Name);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            loggerInstance.LogError(ex, "Сбой при создании записи {ModelName}", typeof(TModel).Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Обновляет существующую запись сущности по идентификатору
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TModel> UpdateItem([FromBody] TDataDto payload, TIdentifier id)
    {
        try
        {
            loggerInstance.LogInformation("Обновление записи {ModelName} с идентификатором {Id}", typeof(TModel).Name, id);
            var updated = appService.Update(payload, id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            loggerInstance.LogWarning(ex, "Запись или связь не найдена при обновлении {ModelName} с ID {Id}", typeof(TModel).Name, id);
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            loggerInstance.LogWarning(ex, "Неверные параметры при обновлении {ModelName}", typeof(TModel).Name);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            loggerInstance.LogError(ex, "Сбой при обновлении записи {ModelName} с ID {Id}", typeof(TModel).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удаляет запись сущности по идентификатору
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteItem(TIdentifier id)
    {
        try
        {
            loggerInstance.LogInformation("Удаление записи {ModelName} с идентификатором {Id}", typeof(TModel).Name, id);
            
            var success = appService.Delete(id);
            if (!success)
            {
                loggerInstance.LogWarning("Невозможно удалить запись {ModelName} с ID {Id}", typeof(TModel).Name, id);
                return Conflict();
            }
            
            return NoContent();
        }
        catch (Exception ex)
        {
            loggerInstance.LogError(ex, "Сбой при удалении записи {ModelName} с ID {Id}", typeof(TModel).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}