using FitnessClub.Domain.Enums;

namespace FitnessClub.Application.Contracts.Clients;

public record ClientDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    DateOnly MembershipStartDate,
    DateOnly MembershipEndDate);