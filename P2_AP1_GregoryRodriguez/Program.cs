using Microsoft.EntityFrameworkCore;
using P2_AP1_GregoryRodriguez.Components;
using P2_AP1_GregoryRodriguez.DAL;
using P2_AP1_GregoryRodriguez.Services;

namespace P2_AP1_GregoryRodriguez;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var ConnectionString = builder.Configuration.GetConnectionString("ConStr");

        builder.Services.AddDbContextFactory<Contexto>(option => option.UseSqlite(ConnectionString));
        builder.Services.AddScoped<RegistroService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
