namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент клуба
/// </summary>
public class Member : Person
{
    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата начала действия абонемента
    /// </summary>
    public required DateOnly MembershipStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента
    /// </summary>
    public required DateOnly MembershipEndDate { get; set; }

    /// <summary>
    /// Проверка просроченности абонемента
    /// </summary>
    public bool IsMembershipExpired(DateOnly currentDate) => currentDate > MembershipEndDate;
}