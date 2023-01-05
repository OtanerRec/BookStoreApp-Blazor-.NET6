using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly ApiAuthenticationStateProvider _apiAuthenticationStateProvider;

        public AuthenticationService(
            IClient httpClient,
            ILocalStorageService localStorage,
            ApiAuthenticationStateProvider apiAuthenticationStateProvider)
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _apiAuthenticationStateProvider = apiAuthenticationStateProvider;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel)
        {
            Response<AuthResponse> response;

            try
            {
                var result = await _httpClient.LoginAsync(loginModel);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true,
                };

                //Store Token
                await _localStorage.SetItemAsync("accessToken", result.Token);

                //Change auth state of app
                await ((ApiAuthenticationStateProvider)_apiAuthenticationStateProvider).LoggedIn();
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<AuthResponse>(exception);
            }

            return response;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_apiAuthenticationStateProvider).LoggedOut();
        }
    }
}
