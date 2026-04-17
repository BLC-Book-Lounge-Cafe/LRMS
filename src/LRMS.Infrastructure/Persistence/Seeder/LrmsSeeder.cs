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
        AddMenu();
    }

    private void AddSpaceSettings()
    {
        if (_dbContext.SpaceSettings.Any())
            return;

        var spaceSettings = new SpaceSettingsEntity
        {
            StartTime = DateTime.SpecifyKind(new(new DateOnly(), new TimeOnly(9, 0)), DateTimeKind.Utc),
            EndTime = DateTime.SpecifyKind(new(new DateOnly(), new TimeOnly(21, 0)), DateTimeKind.Utc),
            MinTableReservationTime = 60,
            TableReservationPeriod = 30,
            BookReservationPeriod = 30
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
                ImagePath = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Преступление и наказание",
                Author = "Ф. Достоевский",
                ImagePath = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Игра престолов",
                Author = "Дж. Мартин",
                ImagePath = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=400&h=600&fit=crop",
            },
            new()
            {
                Name = "Гарри Поттер и философский камень",
                Author = "Дж. Роулинг",
                ImagePath = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Психология лжи",
                Author = "П. Экман",
                ImagePath = "https://images.unsplash.com/photo-1524578271613-d550eacf6090?w=400&h=600&fit=crop"
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
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 6,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 4,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 2,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 7,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                SeatsCount = 2,
                CreatedAt = DateTime.UtcNow
            },
        };

        _dbContext.AddRange(tables);
        _dbContext.SaveChanges();
    }

    private void AddMenu()
    {
        if (_dbContext.MenuItems.Any())
            return;

        var menuCategories = new List<MenuCategoryEntity>
        {
            new()
            {
                Id = 1,
                Name = "Напитки"
            },
            new()
            {
                Id = 2,
                Name = "Десерты"
            },
            new()
            {
                Id = 3,
                Name = "Сэндвичи"
            },
        };

        var menuItems = new List<MenuItemEntity>
        {
            new()
            {
                Name = "Эспрессо",
                Price = 150,
                CategoryId = 1
            },
            new()
            {
                Name = "Американо",
                Price = 160,
                CategoryId = 1
            },
            new()
            {
                Name = "Капучино",
                Price = 170,
                CategoryId = 1
            },
            new()
            {
                Name = "Латте",
                Price = 180,
                CategoryId = 1
            },
            new()
            {
                Name = "Раф",
                Price = 190,
                CategoryId = 1
            },
            new()
            {
                Name = "Чизкейк",
                Price = 300,
                CategoryId = 2
            },
            new()
            {
                Name = "Тирамису",
                Price = 200,
                CategoryId = 2
            },
            new()
            {
                Name = "Брауни",
                Price = 190,
                CategoryId = 2
            },
            new()
            {
                Name = "Макарон",
                Price = 150,
                CategoryId = 2
            },
            new()
            {
                Name = "Эклер",
                Price = 290,
                CategoryId = 2
            },
            new()
            {
                Name = "Сэндвич с курицей",
                Price = 400,
                CategoryId = 3
            },
            new()
            {
                Name = "Сэндвич с лососем",
                Price = 350,
                CategoryId = 3
            }
        };

        _dbContext.MenuCategories.AddRange(menuCategories);
        _dbContext.MenuItems.AddRange(menuItems);
        _dbContext.SaveChanges();
    }
}
