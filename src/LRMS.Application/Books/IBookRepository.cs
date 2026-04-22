namespace LRMS.Application.Books;

public interface IBookRepository
{
    Task CreateBook(string name, string author, string imageUrl, CancellationToken ct = default);
    Task DeleteBook(long id, CancellationToken ct = default);
}
