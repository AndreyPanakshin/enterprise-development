namespace FitnessClub.Domain;

/// <summary>
/// Клиент клуба
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// Дата начала действия абонемента
    /// </summary>
    public required DateTime MembershipStartDate { get; set; }
    /// <summary>
    /// Дата окончания действия абонемента
    /// </summary>
    public required DateTime MembershipEndDate { get; set; }
    /// <summary>
    /// Проверка просроченности абонемента
    /// </summary>
    public bool IsMembershipExpired(DateTime currentDate) => currentDate > MembershipEndDate;
}