using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly ApiAuthenticationStateProvider _apiAuthenticationStateProvider;

        public AuthenticationService(
            IClient httpClient,
            ILocalStorageService localStorage,
            ApiAuthenticationStateProvider apiAuthenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _apiAuthenticationStateProvider = apiAuthenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await _httpClient.LoginAsync(loginModel);

            //Store Token
            await _localStorage.SetItemAsync("accessToken", response.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)_apiAuthenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)_apiAuthenticationStateProvider).LoggedOut();
        }
    }
}
