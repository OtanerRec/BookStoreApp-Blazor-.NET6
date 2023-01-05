using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsRepository(
            BookStoreDbContext context, 
            IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id)
        {
            var author = await _context.Authors
                .Include(q => q.Books)
                .ProjectTo<AuthorDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            return author;
        }
    }
}
