using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Book;

namespace BookStoreApp.Api.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<List<BookReadOnlyDTO>> GetAllBooksAsync();

        Task<BookDetailsDTO> GetBookAsync(int id);
    }
}
