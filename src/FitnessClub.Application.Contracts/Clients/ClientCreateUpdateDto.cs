using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Clients;

public record ClientCreateUpdateDto(
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    DateOnly StartSub,
    DateOnly EndSub);