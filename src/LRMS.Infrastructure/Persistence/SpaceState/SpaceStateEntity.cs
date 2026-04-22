using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.SpaceState;

[Table("space_state")]
public class SpaceStateEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("noise_level")]
    public byte NoiseLevel { get; set; }

    [Column("description")]
    public required string Description { get; set; }

    [Column("current_track", TypeName = "jsonb")]
    public required string CurrentTrack { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
