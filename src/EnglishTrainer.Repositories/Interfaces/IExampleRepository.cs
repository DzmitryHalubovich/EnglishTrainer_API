using EnglishTrainer.Entities.Models;
using EnglishTrainer.Entities.RequestFeatures;

namespace EnglishTrainer.Repositories.Interfaces
{
    public interface IExampleRepository
    {
        Task<PagedList<Example>> GetExamplesAsync(Guid wordId, 
            ExampleParameters exampleParameters ,bool trackChanges);
        Task<Example> GetAsync(Guid wordId, Guid id, bool trackChanges);
        void CreateForWord(Guid wordId, Example example);
        void DeleteExample(Example example);
    }
}
