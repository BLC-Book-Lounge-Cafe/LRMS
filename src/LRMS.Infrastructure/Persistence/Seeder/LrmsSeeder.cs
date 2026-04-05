using LRMS.Application.Dto;
using LRMS.Infrastructure.Persistence.Models;
using System.Text.Json;

namespace LRMS.Infrastructure.Persistence.Seeder;

internal class LrmsSeeder(LrmsDbContext dbContext)
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public void Seed()
    {
        AddSpaceSettings();
        AddSpaceState();
        AddBooks();
        AddTables();
    }

    private void AddSpaceSettings()
    {
        if (_dbContext.SpaceSettings.Any())
            return;

        var spaceSettings = new SpaceSettingsEntity
        {
            StartTime = new(9, 0),
            EndTime = new(21, 0),
            MinTableReservationTime = TimeSpan.FromMinutes(60)
        };

        _dbContext.SpaceSettings.Add(spaceSettings);
        _dbContext.SaveChanges();
    }

    private void AddSpaceState()
    {
        if (_dbContext.SpaceStates.Any())
            return;

        var spaceState = new SpaceStateEntity
        {
            NoiseLevel = 70,
            Description = "Идет дождь, в зале тепло и пахнет корицей. Идеальное время для чтения с чашкой какао.",
            CurrentTrack = JsonSerializer.Serialize(new CurrentTrackDto("Силуэт", ["Ваня Дмитриенко", "Аня Пересильд"],
                "https://cdn-images.dzcdn.net/images/cover/dfc147ae4276f7aa692ae444f8c5300f/500x500-000000-80-0-0.jpg")),
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.SpaceStates.Add(spaceState);
        _dbContext.SaveChanges();
    }

    private void AddBooks()
    {
        if (_dbContext.Books.Any())
            return;

        var books = new List<BookEntity>
        {
            new()
            {
                Name = "Война и мир",
                Author = "Л. Толстой",
                Description = "Для тех, кто пока не определился, что интереснее история или роман"
            },
            new()
            {
                Name = "Преступление и наказание",
                Author = "Ф. Достоевский",
                Description = "Для тех, кто право имеет"
            },
            new()
            {
                Name = "Игра престолов",
                Author = "Дж. Мартин",
                Description = "Для тех, кто ждет зиму"
            },
            new()
            {
                Name = "Гарри Поттер и философский камень",
                Author = "Дж. Роулинг",
                Description = "Для тех, кто не может найти романы в издании Росмэн"
            },
            new()
            {
                Name = "Психология лжи",
                Author = "П. Экман",
                Description = "Для тех, кто узнает лжеца по выражению лица"
            },
            new()
            {
                Name = "Три товарища",
                Author = "Э. Ремарк",
                Description = "Для тех, кто знает, что три товарища - это не Эрих, Мария и Ремарк"
            },
            new()
            {
                Name = "Дюна",
                Author = "Ф. Герберт"
            },
            new()
            {
                Name = "Мертвые души",
                Author = "Н. Гоголь"
            }
        };

        _dbContext.AddRange(books);
        _dbContext.SaveChanges();
    }

    private void AddTables()
    {
        if (_dbContext.Tables.Any())
            return;

        var tables = new List<TableEntity>
        {
            new()
            {
                SeatsCount = 2,
                Description = "Ваш тихий угол на сегодня",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 6,
                Description = "Столик для дружной компании",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 4,
                Description = "Здесь можно заказать вкусный кофе",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 2,
                Description = "Ваш тихий угол",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 7,
                Description = "Столик рядом с баром",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 2,
                Description = "Столик с отличным WIFI",
                CreatedAt = DateTime.UtcNow
            },
        };

        _dbContext.AddRange(tables);
        _dbContext.SaveChanges();
    }
}
