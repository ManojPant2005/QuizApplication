using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using QuizApplication.Client;
using QuizApplication.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorQuizWASM.ServerAPI"));


builder.Services.AddSingleton<MediaStateContainer>();
builder.Services.AddScoped<QuestionStateContainer>();
builder.Services.AddScoped<QuizItemService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<AnswerService>();




builder.Services.AddMudServices();



await builder.Build().RunAsync();
