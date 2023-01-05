using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;
        public AuthorService(
            IClient client,
            ILocalStorageService localStorage,
            IMapper mapper)
            : base(client, localStorage)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDTO author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorsPOSTAsync(author);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<int>> EditAuthor(int id, AuthorUpdateDTO author)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(id, author);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }

        public async Task<Response<AuthorDetailsDTO>> GetAuthor(int id)
        {
            Response<AuthorDetailsDTO> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<AuthorDetailsDTO>(exception);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadOnlyDTO>>> GetAuthors()
        {
            Response<List<AuthorReadOnlyDTO>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<AuthorReadOnlyDTO>>(exception);
            }

            return response;
        }

        public async Task<Response<AuthorUpdateDTO>> GetAuthorForUpdate(int id)
        {
            Response<AuthorUpdateDTO> response;

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response = new Response<AuthorUpdateDTO>
                {
                    Data = _mapper.Map<AuthorUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<AuthorUpdateDTO>(exception);
            }

            return response;
        }

        public async Task<Response<int>> DeleteAuthor(int id)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.AuthorsDELETEAsync(id);
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }
    }
}
