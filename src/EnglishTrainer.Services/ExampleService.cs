using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Services
{
    public class ExampleService : ServiceBase<Example>, IExampleService
    {
        public ExampleService(EFContext dbContext) : base(dbContext) { }

        public IEnumerable<Example> GetAll(Guid wordId, bool trackChanges) =>
             FindByCondition(e=>e.WordId.Equals(wordId),trackChanges)
            .OrderBy(e=>e.Id);
    }
}
