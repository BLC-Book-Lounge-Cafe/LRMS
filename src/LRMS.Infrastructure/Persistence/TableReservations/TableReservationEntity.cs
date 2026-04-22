using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.TableReservations;

[Table("table_reservations")]
public class TableReservationEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_name", TypeName = "varchar(255)")]
    public required string CustomerName { get; set; }

    [Column("customer_phone", TypeName = "varchar(30)")]
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
