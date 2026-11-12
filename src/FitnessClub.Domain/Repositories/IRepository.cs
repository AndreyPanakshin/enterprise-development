namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Универсальный интерфейс репозитория для работы с доменными сущностями
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    TEntity? Get(TKey id);
    IReadOnlyList<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    bool Delete(TKey id);
}