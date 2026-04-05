using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("table_reservations")]
public class TableReservationEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_name")]
    public required string CustomerName { get; set; }

    [Column("customer_phone")]
    public required string CustomerPhone { get; set; }

    [Column("table_id")]
    public long TableId { get; set; }

    [Column("reservation_start_at")]
    public DateTime ReservationStartAt { get; set; }

    [Column("reservation_end_at")]
    public DateTime ReservationEndAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
