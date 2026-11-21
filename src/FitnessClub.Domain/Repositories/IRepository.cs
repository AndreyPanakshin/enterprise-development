namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Универсальный интерфейс репозитория для работы с доменными сущностями
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>Получает сущность по идентификатору</summary>
    public Task<TEntity?> Get(TKey id);

    /// <summary>Получает все сущности</summary>
    public Task<IReadOnlyList<TEntity>> GetAll();

    /// <summary>Добавляет сущность</summary>
    public Task Add(TEntity entity);

    /// <summary>Обновляет сущность</summary>
    public Task Update(TEntity entity);

    /// <summary>Удаляет сущность по идентификатору</summary>
    public Task<bool> Delete(TKey id);
}