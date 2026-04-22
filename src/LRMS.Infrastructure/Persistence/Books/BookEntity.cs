using System.ComponentModel.DataAnnotations.Schema;

namespace LRMS.Infrastructure.Persistence.Books;

[Table("books")]
public class BookEntity
{
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("author")]
    public required string Author { get; set; }

    [Column("image_path")]
    public string ImagePath { get; set; }
}
