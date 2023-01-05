using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.DTO.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BooksRepository(
            BookStoreDbContext context, 
            IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookReadOnlyDTO>> GetAllBooksAsync()
        {
            return await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadOnlyDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<BookDetailsDTO> GetBookAsync(int id)
        {
            return await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailsDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
