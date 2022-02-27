using BlazingTrails.Client;
using BlazingTrails.Client.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SecureAPIClient",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
//        .CreateClient("SecureAPIClient"));

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:ApiIdentifier"]);
    //options.UserOptions.NameClaim = ClaimTypes.GivenName; // S�tter name prop til givenname, default er email
})
    .AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();
