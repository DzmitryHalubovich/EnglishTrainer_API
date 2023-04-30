using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Repositories.Interfaces
{
    public interface IExampleRepository
    {
        Task<IEnumerable<Example>> GetAllAsync(Guid wordId, bool trackChanges);

        Task<Example> GetAsync(Guid wordId, Guid id, bool trackChanges);
        void CreateForWord(Guid wordId, Example example);
        void DeleteExample(Example example);
    }
}
