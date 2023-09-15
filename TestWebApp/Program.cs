using Newtonsoft.Json;
using TestWebApp.Models;

namespace TestWebApp;

public class Program
{
    public static void Main (string[] args)
    {



        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Use(async (context, next) => {
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            await next();
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        });

        app.Use(async (context, next) => {
            try {
                await next();
            }
            catch (Exception e) {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new { error = e.Message };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        });

        var test = new ProductTest();
        test.StartTest();

        app.Run();
    }
}
