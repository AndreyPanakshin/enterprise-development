using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

public record TrainerDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    int SpecializationId,
    int ExperienceYears);