using EverythingInCSharp.Muddle;
using EverythingInCSharp.Muddle.Game;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<Game>();
builder.Services.AddSingleton<GameInput>();
builder.Services.AddSingleton<AnswerProvider>();
builder.Services.AddSingleton<IAnswerSource>(
    sp => {
        var httpClient = sp.GetRequiredService<HttpClient>();
        const string filePath = "word-list-debug.txt";
        return new HttpFileAnswerSource(httpClient, filePath);
    });
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
