using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.Repositories.Ef;

/// <summary>
/// Фабрика контекста для создания миграций на этапе разработки
/// </summary>
public class FitnessClubDbContextFactory : IDesignTimeDbContextFactory<FitnessClubDbContext>
{
    /// <summary>Создает контекст с настройками MySQL для генерации миграций</summary>
    public FitnessClubDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseMySql(
                "Server=localhost;Database=fitnessclub;User=root;Password=root",
                MySqlServerVersion.LatestSupportedServerVersion)
            .Options;
        return new FitnessClubDbContext(options);
    }
}