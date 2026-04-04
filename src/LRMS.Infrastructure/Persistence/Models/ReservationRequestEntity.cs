using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("reservation_requests")]
public class ReservationRequestEntity
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("customer_name")]
    public required string CustomerName { get; set; }

    [Column("customer_phone")]
    public required string CustomerPhone { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
