using LRMS.Application.SpaceState.Dto;
using LRMS.Infrastructure.Persistence.Books;
using LRMS.Infrastructure.Persistence.Menu;
using LRMS.Infrastructure.Persistence.SpaceState;
using LRMS.Infrastructure.Persistence.Tables;
using System.Text.Json;

namespace LRMS.Infrastructure.Persistence.Seeder;

public class LrmsSeeder(LrmsDbContext dbContext)
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public void Seed()
    {
        AddSpaceState();
        AddBooks();
        AddTables();
        AddMenu();
    }

    private void AddSpaceState()
    {
        if (_dbContext.SpaceStates.Any())
            return;

        var spaceState = new SpaceStateEntity
        {
            NoiseLevel = (byte)NoiseLevelType.Lively,
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
                ImagePath = "https://cdn.azbooka.ru/cv/w1100/3f68a41d-d7ec-4f1b-ae7b-36376eb66430.jpg"
            },
            new()
            {
                Name = "Гарри Поттер и Тайная комната",
                Author = "Дж. Роулинг",
                ImagePath = "https://imo10.labirint.ru/books/435204/cover.jpg/363-0"
            },
            new()
            {
                Name = "Гарри Поттер и узник Азкабана",
                Author = "Дж. Роулинг",
                ImagePath = "https://imo10.labirint.ru/books/911380/cover.jpg/363-0"
            },
            new()
            {
                Name = "Властелин колец: Братство кольца",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://imo10.labirint.ru/books/899194/cover.jpg/363-0"
            },
            new()
            {
                Name = "Властелин колец: Две крепости",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://imo10.labirint.ru/books/343795/cover.jpg/363-0"
            },
            new()
            {
                Name = "Властелин колец: Возвращение короля",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://imo10.labirint.ru/books/899196/cover.jpg/363-0"
            },
            new()
            {
                Name = "Хоббит, или Туда и обратно",
                Author = "Дж. Р. Р. Толкин",
                ImagePath = "https://imo10.labirint.ru/books/479670/cover.jpg/363-0"
            },
            new()
            {
                Name = "451 градус по Фаренгейту",
                Author = "Рэй Брэдбери",
                ImagePath = "https://imo10.labirint.ru/books/773894/cover.jpg/363-0"
            },
            new()
            {
                Name = "1984",
                Author = "Джордж Оруэлл",
                ImagePath = "https://imo10.labirint.ru/books/790566/cover.jpg/363-0"
            },
            new()
            {
                Name = "Скотный двор",
                Author = "Джордж Оруэлл",
                ImagePath = "https://imo10.labirint.ru/books/666748/cover.jpg/363-0"
            },
            new()
            {
                Name = "Преступление и наказание",
                Author = "Фёдор Достоевский",
                ImagePath = "https://imo10.labirint.ru/books/863342/cover.jpg/363-0"
            },
            new()
            {
                Name = "Война и мир",
                Author = "Лев Толстой",
                ImagePath = "https://imo10.labirint.ru/books/832939/cover.jpg/363-0"
            },
            new()
            {
                Name = "Анна Каренина",
                Author = "Лев Толстой",
                ImagePath = "https://imo10.labirint.ru/books/603972/cover.jpg/363-0"
            },
            new()
            {
                Name = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                ImagePath = "https://imo10.labirint.ru/books/942043/cover.jpg/363-0"
            },
            new()
            {
                Name = "Убить пересмешника",
                Author = "Харпер Ли",
                ImagePath = "https://imo10.labirint.ru/books/594261/cover.jpg/363-0"
            },
            new()
            {
                Name = "Великий Гэтсби",
                Author = "Фрэнсис Скотт Фицджеральд",
                ImagePath = "https://imo10.labirint.ru/books/850729/cover.jpg/363-0"
            },
            new()
            {
                Name = "Над пропастью во ржи",
                Author = "Джером Д. Сэлинджер",
                ImagePath = "https://imo10.labirint.ru/books/1011596/cover.jpg/363-0"
            },
            new()
            {
                Name = "Моби Дик",
                Author = "Герман Мелвилл",
                ImagePath = "https://cdn.azbooka.ru/cv/w383/webp/3cbe0a3b-03fb-4872-ac70-e1e6ad106e09.webp"
            },
            new()
            {
                Name = "Гордость и предубеждение",
                Author = "Джейн Остин",
                ImagePath = "https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/fa8474e9-35ca-447c-9dca-baf879ccaa9e/600x900"
            },
            new()
            {
                Name = "Джейн Эйр",
                Author = "Шарлотта Бронте",
                ImagePath = "https://imo10.labirint.ru/books/549739/cover.jpg/484-0"
            },
            new()
            {
                Name = "Грозовой перевал",
                Author = "Эмили Бронте",
                ImagePath = "https://imo10.labirint.ru/books/771312/cover.jpg/484-0"
            },
            new()
            {
                Name = "Отверженные",
                Author = "Виктор Гюго",
                ImagePath = "https://cdn.azbooka.ru/cv/w383/webp/926299db-bc91-4f55-85d5-5c4707f71243.webp"
            },
            new()
            {
                Name = "Собор Парижской Богоматери",
                Author = "Виктор Гюго",
                ImagePath = "https://imo10.labirint.ru/books/871948/cover.jpg/484-0"
            },
            new()
            {
                Name = "Три мушкетёра",
                Author = "Александр Дюма",
                ImagePath = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/3ab9da68-46bf-4213-b79d-21cd0d8fb5b7/600x900"
            },
            new()
            {
                Name = "Граф Монте-Кристо",
                Author = "Александр Дюма",
                ImagePath = "https://www.mann-ivanov-ferber.ru/assets/images/covers/89/34089/1.50x-thumb.png"
            },
            new()
            {
                Name = "Дон Кихот",
                Author = "Мигель де Сервантес",
                ImagePath = "https://avatars.mds.yandex.net/get-ott/1652588/2a0000019850f1b54b46ecc6719c6aa6e24f/600x900"
            },
            new()
            {
                Name = "Ромео и Джульетта",
                Author = "Уильям Шекспир",
                ImagePath = "https://cdn.azbooka.ru/cv/w383/webp/b42d59bd-9c48-4a8c-8f63-2cdddb4b7392.webp"
            },
            new()
            {
                Name = "Алиса в Стране чудес",
                Author = "Льюис Кэрролл",
                ImagePath = "https://www.moscowbooks.ru/image/book/720/w259/i720889.jpg?cu=20210525164602"
            },
            new()
            {
                Name = "Питер Пэн",
                Author = "Джеймс Мэттью Барри",
                ImagePath = "https://nukadeti.ru/content/images/static/tale400x400_m/6301_135.webp"
            },
            new()
            {
                Name = "Винни-Пух",
                Author = "Алан Милн",
                ImagePath = "https://upload.wikimedia.org/wikipedia/ru/1/11/Winnie_Pooh.jpg"
            },
            new()
            {
                Name = "Маленький принц",
                Author = "Антуан де Сент-Экзюпери",
                ImagePath = "https://artnow.ru/img/1515000/1515118.jpg"
            },
            new()
            {
                Name = "Чума",
                Author = "Альбер Камю",
                ImagePath = "https://imo10.labirint.ru/books/452840/cover.jpg/484-0"
            },
            new()
            {
                Name = "Посторонний",
                Author = "Альбер Камю",
                ImagePath = "https://imo10.labirint.ru/books/441765/cover.jpg/484-0"
            },
            new()
            {
                Name = "Процесс",
                Author = "Франц Кафка",
                ImagePath = "https://imo10.labirint.ru/books/790570/cover.jpg/363-0"
            },
            new()
            {
                Name = "Замок",
                Author = "Франц Кафка",
                ImagePath = "https://imo10.labirint.ru/books/901217/cover.jpg/363-0"
            },
            new()
            {
                Name = "Любовь во время холеры",
                Author = "Габриэль Гарсиа Маркес",
                ImagePath = "https://imo10.labirint.ru/books/772096/cover.jpg/363-0"
            },
            new()
            {
                Name = "Бойня номер пять",
                Author = "Курт Воннегут",
                ImagePath = "https://imo10.labirint.ru/books/728183/cover.jpg/363-0"
            },
            new()
            {
                Name = "Колыбель для кошки",
                Author = "Курт Воннегут",
                ImagePath = "https://imo10.labirint.ru/books/592559/cover.jpg/363-0"
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
                SeatsCount = 2
            },
            new()
            {
                SeatsCount = 6
            },
            new()
            {
                SeatsCount = 4
            },
            new()
            {
                SeatsCount = 2
            },
            new()
            {
                SeatsCount = 7
            },
            new()
            {
                SeatsCount = 2
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
                Name = "Напитки"
            },
            new()
            {
                Name = "Десерты"
            },
            new()
            {
                Name = "Сэндвичи"
            },
        };

        _dbContext.MenuCategories.AddRange(menuCategories);
        _dbContext.SaveChanges();

        var menuItems = new List<MenuItemEntity>
        {
            new()
            {
                Name = "Эспрессо",
                Price = 150,
                CategoryId = menuCategories[0].Id
            },
            new()
            {
                Name = "Американо",
                Price = 160,
                CategoryId = menuCategories[0].Id
            },
            new()
            {
                Name = "Капучино",
                Price = 170,
                CategoryId = menuCategories[0].Id
            },
            new()
            {
                Name = "Латте",
                Price = 180,
                CategoryId = menuCategories[0].Id
            },
            new()
            {
                Name = "Раф",
                Price = 190,
                CategoryId = menuCategories[0].Id
            },
            new()
            {
                Name = "Чизкейк",
                Price = 300,
                CategoryId = menuCategories[1].Id
            },
            new()
            {
                Name = "Тирамису",
                Price = 200,
                CategoryId = menuCategories[1].Id
            },
            new()
            {
                Name = "Брауни",
                Price = 190,
                CategoryId = menuCategories[1].Id
            },
            new()
            {
                Name = "Макарон",
                Price = 150,
                CategoryId = menuCategories[1].Id
            },
            new()
            {
                Name = "Эклер",
                Price = 290,
                CategoryId = menuCategories[1].Id
            },
            new()
            {
                Name = "Сэндвич с курицей",
                Price = 400,
                CategoryId = menuCategories[2].Id
            },
            new()
            {
                Name = "Сэндвич с лососем",
                Price = 350,
                CategoryId = menuCategories[2].Id
            }
        };

        _dbContext.MenuItems.AddRange(menuItems);
        _dbContext.SaveChanges();
    }
}
