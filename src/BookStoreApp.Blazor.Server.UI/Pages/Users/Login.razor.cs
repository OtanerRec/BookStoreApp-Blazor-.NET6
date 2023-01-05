using System;
using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Pages.Users
{
    public partial class Login
    {
        [Inject] IAuthenticationService _authService { get; set; }
        [Inject] NavigationManager _navManager { get; set; }

        LoginUserDto LoginModel = new LoginUserDto();
        string message = string.Empty;

        private async Task HandleLogin()
        {
            var response = await _authService.AuthenticateAsync(LoginModel);

            if (response.Success)
            {
                _navManager.NavigateTo("/");
            }

            message = response.Message;
        }
    }
}
