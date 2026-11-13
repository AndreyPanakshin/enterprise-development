using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для персональных тренировок
/// </summary>
/// <param name="sessionSvc">Сервис для выполнения операций с тренировками</param>
/// <param name="logger">Логгер контроллера</param>
public class TrainingSessionsController(
    IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int> sessionSvc,
    ILogger<TrainingSessionsController> logger)
    : BaseEntityController<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>(sessionSvc, logger);