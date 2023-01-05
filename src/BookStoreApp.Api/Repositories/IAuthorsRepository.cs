using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Author;

namespace BookStoreApp.Api.Repositories
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id);
    }
}
