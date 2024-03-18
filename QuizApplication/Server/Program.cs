
using BlazorQuizWASM.Server.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuizApplication.Server.Data;
using QuizApplication.Server.Models;
using QuizApplication.Server.Repositories;

namespace BlazorQuizWASM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz API", Version = "v1" });



            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddTransient<IQuestionRepository, SQLQuestionRepository>();
            builder.Services.AddTransient<IAnswerRepository, SQLAnswerRepository>();
            builder.Services.AddTransient<IQuizItemRepository, SQLQuizItemRepository>();
            builder.Services.AddTransient<IMediaTypeRepository, SQLMediaTypeRepository>();

        



            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

                });

            builder.Services.AddRazorPages();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true)
                           .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowEverything");

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}