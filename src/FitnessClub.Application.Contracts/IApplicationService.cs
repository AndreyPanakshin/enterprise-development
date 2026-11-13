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
    public IList<TModel> GetAll();

    /// <summary>Получает запись по уникальному идентификатору</summary>
    public TModel? Get(TKey id);

    /// <summary>Создает новую запись</summary>
    public TModel Create(TPayload payload);

    /// <summary>Обновляет существующую запись по идентификатору</summary>
    public TModel Update(TPayload payload, TKey id);

    /// <summary>Удаляет запись по идентификатору. Возвращает true при успехе</summary>
    public bool Delete(TKey id);
}