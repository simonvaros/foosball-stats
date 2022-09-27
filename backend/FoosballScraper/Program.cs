using System.Collections.Generic;
using System.IO;
using Database;
using FoosballScraper.Models;
using FoosballScraper.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FoosballScraper;

class AppConfig
{
    public string DatabaseConnectionString { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddEnvironmentVariables();
        var configurationRoot = builder.Build();
        var appConfig = configurationRoot.GetSection(nameof(AppConfig)).Get<AppConfig>();
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(appConfig.DatabaseConnectionString);
        
        var databaseContext = new DatabaseContext(optionsBuilder.Options);
        databaseContext.Database.Migrate();
        
        var totalEvents = GetEventsFromFile();

        ScrapingService.ScrapeFoosball(totalEvents);
        DatabaseService.CleanDatabase(databaseContext);
        DatabaseService.AddPlayersFromJsonToDb(databaseContext, totalEvents);
        DatabaseService.AddEventsFromJsonToDb(databaseContext, totalEvents);
        DatabaseService.AddTeamsFromJsonToDb(databaseContext, totalEvents);
        DatabaseService.AddEventTeamsFromJsonToDb(databaseContext, totalEvents);
        StatsService.CountStatisticsForPlayers(databaseContext);
    }

    static List<Event> GetEventsFromFile()
    {
        var fileName = "TournamentsWithTeams.json";
        if (!File.Exists(fileName))
        {
            return new List<Event>();
        }
        
        var jsonString = File.ReadAllText(fileName);
        var events = JsonConvert.DeserializeObject<List<Event>>(jsonString);

        return events;
    }
}