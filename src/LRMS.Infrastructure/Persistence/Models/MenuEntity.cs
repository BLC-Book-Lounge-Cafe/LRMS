using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Models;

[Table("menu")]
public class MenuEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("image_path")]
    public required string ImagePath { get; set; }
}
