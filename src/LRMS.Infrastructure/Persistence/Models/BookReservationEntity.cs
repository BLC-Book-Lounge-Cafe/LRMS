using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("book_reservations")]
public class BookReservationEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_name", TypeName = "varchar(255)")]
    public required string CustomerName { get; set; }

    [Column("customer_phone", TypeName = "varchar(30)")]
    public required string CustomerPhone { get; set; }

    [Column("book_id")]
    public long BookId { get; set; }

    [Column("reservation_start_at")]
    public DateTime ReservationStartAt { get; set; }

    [Column("reservation_end_at")]
    public DateTime ReservationEndAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
