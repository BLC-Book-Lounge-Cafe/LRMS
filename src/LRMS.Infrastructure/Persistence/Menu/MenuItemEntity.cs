using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Menu;

[Table("menu_items")]
public class MenuItemEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("price")]
    public double Price { get; set; }
}
