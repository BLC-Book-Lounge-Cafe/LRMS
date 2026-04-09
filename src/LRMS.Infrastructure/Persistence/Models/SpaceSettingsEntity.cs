using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("space_settings")]
public class SpaceSettingsEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("start_time")]
    public DateTime StartTime {  get; set; }

    [Column("end_time")]
    public DateTime EndTime { get; set; }

    [Column("min_table_reservation_time")]
    public byte MinTableReservationTime { get; set; }

    [Column("table_reservation_period")]
    public byte TableReservationPeriod { get; set; }

    [Column("book_reservation_period")]
    public byte BookReservationPeriod { get; set; }
}
