using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDTO>>> GetBooks();

        Task<Response<BookDetailsDTO>> GetBook(int id);

        Task<Response<BookUpdateDTO>> GetBookForUpdate(int id);

        Task<Response<int>> CreateBook(BookCreateDTO book);

        Task<Response<int>> EditBook(int id, BookUpdateDTO book);

        Task<Response<int>> DeleteBook(int id);
    }
}