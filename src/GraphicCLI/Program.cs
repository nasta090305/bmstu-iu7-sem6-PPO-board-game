using DB;
using GameLogic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GUI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("D:\\sem6\\ppo\\lab4\\ppo\\src\\SeaSaltPaper\\ConsoleCLI\\config.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            var serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.File("D:\\sem6\\ppo\\lab4\\ppo\\src\\SeaSaltPaper\\Logs\\app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.ClearProviders();
            builder.AddSerilog(serilogLogger, dispose: true);
        });

        var logger = loggerFactory.CreateLogger("AppLogger");

        GameManager gameManager = new GameManager(new Game(), new ScoreCalculator(), new GameRepository(connectionString), logger);
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new StartForm(gameManager));
    }
}