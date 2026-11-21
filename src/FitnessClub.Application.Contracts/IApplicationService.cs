namespace FitnessClub.Application.Contracts;

/// <summary>
/// Базовый интерфейс для выполнения CRUD-операций над сущностями
/// </summary>
public interface IApplicationService<TModel, TPayload, TKey>
    where TModel : class
    where TPayload : class
    where TKey : struct
{
    /// <summary>Получает список всех записей</summary>
    public Task<IList<TModel>> GetAll();

    /// <summary>Получает запись по уникальному идентификатору</summary>
    public Task<TModel?> Get(TKey id);

    /// <summary>Создает новую запись</summary>
    public Task<TModel> Create(TPayload payload);

    /// <summary>Обновляет существующую запись по идентификатору</summary>
    public Task<TModel> Update(TPayload payload, TKey id);

    /// <summary>Удаляет запись по идентификатору. Возвращает true при успехе</summary>
    public Task<bool> Delete(TKey id);
}