using PizzaStore.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PizzaStore.Contracts;
using PizzaStore.Client.Services;
using PizzaStore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

builder.Services.AddScoped<IPizzaService, ClientPizzaService>();
builder.Services.AddScoped<IOrdersService, ClientOrdersService>();
builder.Services.AddScoped<OrderState>();

await builder.Build().RunAsync();
