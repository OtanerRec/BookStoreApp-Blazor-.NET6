using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDTO>>> GetAuthors();

        Task<Response<AuthorDetailsDTO>> GetAuthor(int id);

        Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id);

        Task<Response<int>> CreateAuthor(AuthorCreateDTO author);

        Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO author);

        Task<Response<int>> DeleteAuthor(int id);
    }
}