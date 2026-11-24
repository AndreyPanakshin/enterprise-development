using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент клуба
/// </summary>
[Table("members")]
public class Member : Person
{
    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [Column("phone_number")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата начала действия абонемента
    /// </summary>
    [Column("membership_start_date")]
    public required DateOnly MembershipStartDate { get; set; }

    /// <summary>
    /// Дата окончания действия абонемента
    /// </summary>
    [Column("membership_end_date")]
    public required DateOnly MembershipEndDate { get; set; }

    /// <summary>
    /// Проверка просроченности абонемента
    /// </summary>
    public bool IsMembershipExpired(DateOnly currentDate) => currentDate > MembershipEndDate;
}