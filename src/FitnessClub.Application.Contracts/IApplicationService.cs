namespace FitnessClub.Application.Contracts;

public interface IApplicationService<TModel, TPayload, TKey>
    where TModel : class
    where TPayload : class
    where TKey : struct
{
    IList<TModel> GetAll();
    TModel? Get(TKey id);
    TModel Create(TPayload payload);
    TModel Update(TPayload payload, TKey id);
    bool Delete(TKey id);
}