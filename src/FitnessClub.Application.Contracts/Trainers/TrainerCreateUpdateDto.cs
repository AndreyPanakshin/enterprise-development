using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

public record TrainerCreateUpdateDto(
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    int SpecializationId,
    int ExperienceYears);