using Microsoft.Extensions.Logging;

namespace GameLogic;

public interface BaseGameRepository
{
    public Game Load();
    public void Save(Game game);
}


public class DataManager
{
    BaseGameRepository repository;
    ILogger logger;
    public DataManager(BaseGameRepository repository, ILogger logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    internal Game Load()
    {
        logger.LogInformation("Загрузка данных об игре");
        return repository.Load();
    }

    internal void Save(Game game)
    {
        logger.LogInformation("Сохранение игры");
        repository.Save(game);
    }
}
