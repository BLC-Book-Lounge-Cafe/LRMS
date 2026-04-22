using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.ReservationRequests;

[Table("reservation_requests")]
public class ReservationRequestEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_name", TypeName = "varchar(255)")]
    public required string CustomerName { get; set; }

    [Column("customer_phone", TypeName = "varchar(30)")]
    public required string CustomerPhone { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
