using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public BookService(
            IClient client,
            ILocalStorageService localStorage,
            IMapper mapper)
            : base(client, localStorage)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateBook(BookCreateDTO book)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.BooksPOSTAsync(book);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> EditBook(int id, BookUpdateDTO book)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.BooksPUTAsync(id, book);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<BookDetailsDTO>> GetBook(int id)
        {
            Response<BookDetailsDTO> response;

            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id);
                response = new Response<BookDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<BookDetailsDTO>(exception);
            }

            return response;
        }

        public async Task<Response<List<BookReadOnlyDTO>>> GetBooks()
        {
            Response<List<BookReadOnlyDTO>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.BooksAllAsync();
                response = new Response<List<BookReadOnlyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<BookReadOnlyDTO>>(exception);
            }

            return response;
        }

        public async Task<Response<BookUpdateDTO>> GetBookForUpdate(int id)
        {
            Response<BookUpdateDTO> response;

            try
            {
                await GetBearerToken();
                var data = await _client.BooksGETAsync(id);
                response = new Response<BookUpdateDTO>
                {
                    Data = _mapper.Map<BookUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<BookUpdateDTO>(exception);
            }

            return response;
        }

        public async Task<Response<int>> DeleteBook(int id)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.BooksDELETEAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }
    }
}
