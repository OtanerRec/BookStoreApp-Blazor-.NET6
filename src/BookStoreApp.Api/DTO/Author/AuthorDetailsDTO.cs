using System.ComponentModel.DataAnnotations;
using BookStoreApp.Api.DTO.Book;

namespace BookStoreApp.Api.DTO.Author
{
    public class AuthorDetailsDTO : AuthorReadOnlyDTO
    {
        public List<BookReadOnlyDTO> Books { get; set; }
    }
}
