using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("tables")]
public class TableEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("seats_count")]
    public byte SeatsCount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
