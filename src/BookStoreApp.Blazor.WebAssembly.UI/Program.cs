using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI;
using BookStoreApp.Blazor.WebAssembly.UI.Providers;
using BookStoreApp.Blazor.WebAssembly.UI.Services;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7147"));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
    p.GetRequiredService<ApiAuthenticationStateProvider>());

await builder.Build().RunAsync();
