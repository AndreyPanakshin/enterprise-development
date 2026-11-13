using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для персональных тренировок
/// </summary>
public class PersonalTrainingSessionsController(
    IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int> sessionSvc,
    ILogger<PersonalTrainingSessionsController> logger)
    : BaseEntityController<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>(sessionSvc, logger);