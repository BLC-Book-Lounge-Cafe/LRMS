using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("space_settings")]
public class SpaceSettingsEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("start_time")]
    public TimeOnly StartTime {  get; set; }

    [Column("end_time")]
    public TimeOnly EndTime { get; set; }

    [Column("min_table_reservation_time")]
    public TimeSpan MinTableReservationTime { get; set; }
}
