using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using EnglishTrainer.Entities.RequestFeatures;
using EnglishTrainer.Repositories.Extensions;

namespace EnglishTrainer.Repositories.Implemintations
{
    public class ExampleRepository : RepositoryBase<Example>, IExampleRepository
    {
        public ExampleRepository(EFContext dbContext) : base(dbContext) { }

        public void CreateForWord(Guid wordId, Example example)
        {
            example.WordId = wordId;
            Create(example);
        }

        public void DeleteExample(Example example)
        {
            Delete(example);
        }

        public async Task<Example> GetAsync(Guid wordId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.WordId.Equals(wordId)
            && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<PagedList<Example>> GetExamplesAsync(Guid wordId, ExampleParameters exampleParameters,
            bool trackChanges)
        {
            var examples = await FindByCondition(e=>e.WordId.Equals(wordId),trackChanges)
                .Search(exampleParameters.SearchTerm)
                .OrderBy(e=>e.Id)
                .Skip((exampleParameters.PageNumber-1)* exampleParameters.PageSize)
                .Take(exampleParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(e=>e.WordId.Equals(wordId), trackChanges).CountAsync();

            return new PagedList<Example>(examples, count, exampleParameters.PageNumber,
                exampleParameters.PageSize);
        }

    }
}
