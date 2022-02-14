using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Skipass;
using Skipass.Configuration;
using Skipass.Database;
using Skipass.Extenstions;
using Skipass.Workers;
using Skipass.Workers.Handlers;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}{NewLine}\t{Message:lj}{NewLine}\t{Exception}{NewLine}", theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .CreateBootstrapLogger();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    NullValueHandling = NullValueHandling.Include,
    Converters = {
        MessageExtensions.CreateMessagesSubtypes(),
    }
};

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
        configuration
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}{NewLine}\t{Message:lj}{NewLine}\t{Exception}{NewLine}", theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
            .ReadFrom.Services(services)
            .ReadFrom.Configuration(context.Configuration)
        );
    builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        config.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
    );

    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Include,
        Converters = {
                MessageExtensions.CreateMessagesSubtypes(),
            }
    };

    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<CardHistoryRequestHandler>()
                    .AddScoped<CardValidityRequestHandler>()
                    .AddScoped<EntryConfirmationHandler>()
                    .AddScoped<EntryRequestHandler>();

    // builder.Services.AddHostedService<TerminalsWorker>()
    //                 .AddHostedService<ScannersWorker>();

    var dbConfig = builder.Configuration.GetRequiredSection(ConfigPaths.DatabaseSection).Get<DatabaseConfiguration>();

    builder.Services.AddDbContext<SkipassDbContext>(contextOptions =>
        contextOptions.UseNpgsql(dbConfig.ConnectionString, npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable(dbConfig.MigrationsTable, SkipassDbContext.SCHEMA);
            npgsqlOptions.SetPostgresVersion(dbConfig.DatabaseVersion);
        })
    );

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<SkipassDbContext>()
        .AddDefaultUI();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    if (dbConfig.AutoMigrations)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SkipassDbContext>();
        db.Database.Migrate();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SkipassDataInitializer.SeedData(userManager, roleManager);
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}