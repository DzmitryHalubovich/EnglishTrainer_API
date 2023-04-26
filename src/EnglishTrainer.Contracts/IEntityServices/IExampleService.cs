using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Contracts.EntitiesServices
{
    public interface IExampleService
    {
        IEnumerable<Example> GetAll(Guid wordId, bool trackChanges);
    }
}
