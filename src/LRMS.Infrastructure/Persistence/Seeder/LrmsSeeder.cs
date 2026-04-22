using LRMS.Application.SpaceState.Dto;
using LRMS.Infrastructure.Persistence.Books;
using LRMS.Infrastructure.Persistence.Menu;
using LRMS.Infrastructure.Persistence.SpaceSettings;
using LRMS.Infrastructure.Persistence.SpaceState;
using LRMS.Infrastructure.Persistence.Tables;
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
                Name = "Гарри Поттер и философский камень",
                Author = "Дж. Роулинг",
                ImagePath = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Гарри Поттер и Тайная комната",
                Author = "Дж. Роулинг",
                ImagePath = "https://images.unsplash.com/photo-1500462918059-b1a0cb512f1d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Гарри Поттер и узник Азкабана",
                Author = "Дж. Роулинг",
                ImagePath = "https://images.unsplash.com/photo-1506466010722-395aa2bef877?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Властелин колец: Братство кольца",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Властелин колец: Две крепости",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Властелин колец: Возвращение короля",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://images.unsplash.com/photo-1468276311594-df7cb65d8df6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Хоббит, или Туда и обратно",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://images.unsplash.com/photo-1533481405265-e9ce0c044abb?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "451 градус по Фаренгейту",
                Author = "Рэй Брэдбери",
                ImagePath = "https://images.unsplash.com/photo-1495640388908-05fa85288e86?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "1984",
                Author = "Джордж Оруэлл",
                ImagePath = "https://images.unsplash.com/photo-1544716278-e513176f20b5?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Скотный двор",
                Author = "Джордж Оруэлл",
                ImagePath = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Преступление и наказание",
                Author = "Фёдор Достоевский",
                ImagePath = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Война и мир",
                Author = "Лев Толстой",
                ImagePath = "https://images.unsplash.com/photo-1524995997946-a1c2e315a42f?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Анна Каренина",
                Author = "Лев Толстой",
                ImagePath = "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                ImagePath = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Убить пересмешника",
                Author = "Харпер Ли",
                ImagePath = "https://images.unsplash.com/photo-1506880018603-1d42cdb6601d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Великий Гэтсби",
                Author = "Фрэнсис Скотт Фицджеральд",
                ImagePath = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Над пропастью во ржи",
                Author = "Джером Д. Сэлинджер",
                ImagePath = "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Моби Дик",
                Author = "Герман Мелвилл",
                ImagePath = "https://images.unsplash.com/photo-1518837695005-2083093ee35b?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Гордость и предубеждение",
                Author = "Джейн Остин",
                ImagePath = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Джейн Эйр",
                Author = "Шарлотта Бронте",
                ImagePath = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Грозовой перевал",
                Author = "Эмили Бронте",
                ImagePath = "https://images.unsplash.com/photo-1506466010722-395aa2bef877?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Отверженные",
                Author = "Виктор Гюго",
                ImagePath = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Собор Парижской Богоматери",
                Author = "Виктор Гюго",
                ImagePath = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Три мушкетёра",
                Author = "Александр Дюма",
                ImagePath = "https://images.unsplash.com/photo-1468276311594-df7cb65d8df6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Граф Монте-Кристо",
                Author = "Александр Дюма",
                ImagePath = "https://images.unsplash.com/photo-1532012197267-da84d127e765?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Дон Кихот",
                Author = "Мигель де Сервантес",
                ImagePath = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Ромео и Джульетта",
                Author = "Уильям Шекспир",
                ImagePath = "https://images.unsplash.com/photo-1495640388908-05fa85288e86?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Гамлет",
                Author = "Уильям Шекспир",
                ImagePath = "https://images.unsplash.com/photo-1481627834876-b7833e8f5570?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Алиса в Стране чудес",
                Author = "Льюис Кэрролл",
                ImagePath = "https://images.unsplash.com/photo-1506880018603-1d42cdb6601d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Питер Пэн",
                Author = "Джеймс Мэттью Барри",
                ImagePath = "https://images.unsplash.com/photo-1512820790803-83ca734da794?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Винни-Пух",
                Author = "Алан Милн",
                ImagePath = "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Маленький принц",
                Author = "Антуан де Сент-Экзюпери",
                ImagePath = "https://images.unsplash.com/photo-1518837695005-2083093ee35b?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Чума",
                Author = "Альбер Камю",
                ImagePath = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Посторонний",
                Author = "Альбер Камю",
                ImagePath = "https://images.unsplash.com/photo-1500462918059-b1a0cb512f1d?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Процесс",
                Author = "Франц Кафка",
                ImagePath = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Замок",
                Author = "Франц Кафка",
                ImagePath = "https://images.unsplash.com/photo-1533481405265-e9ce0c044abb?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Сто лет одиночества",
                Author = "Габриэль Гарсиа Маркес",
                ImagePath = "https://images.unsplash.com/photo-1506466010722-395aa2bef877?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Любовь во время холеры",
                Author = "Габриэль Гарсиа Маркес",
                ImagePath = "https://images.unsplash.com/photo-1507842217343-583bb7270b66?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Бойня номер пять",
                Author = "Курт Воннегут",
                ImagePath = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=400&h=600&fit=crop"
            },
            new()
            {
                Name = "Колыбель для кошки",
                Author = "Курт Воннегут",
                ImagePath = "https://images.unsplash.com/photo-1468276311594-df7cb65d8df6?w=400&h=600&fit=crop"
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
