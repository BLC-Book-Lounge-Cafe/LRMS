using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Menu;

[Table("menu_categories")]
public class MenuCategoryEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public required string Name { get; set; }
}
