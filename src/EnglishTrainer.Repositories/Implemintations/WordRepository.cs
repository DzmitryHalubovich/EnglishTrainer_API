using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using EnglishTrainer.Entities.RequestFeatures;
using EnglishTrainer.Repositories.Extensions;

namespace EnglishTrainer.Repositories.Implemintations
{
    public class WordRepository : RepositoryBase<Word>, IWordRepository
    {
        public WordRepository(EFContext dbContext) : base(dbContext) { }

        public void CreateWord(Word word) => Create(word);

        public void DeleteWord(Word word)
        {
            Delete(word);
        }

        public async Task<IEnumerable<Word>> GetWordsAsync(
            WordParameters wordParameters, bool trackChanges)
        {
            var words = await FindAll(trackChanges)
                .Search(wordParameters.SearchTerm)
                .OrderBy(w=>w.Name)
                .Skip((wordParameters.PageNumber-1)* wordParameters.PageSize)
                .Take(wordParameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Word>(words, count, wordParameters.PageNumber,
               wordParameters.PageSize);
        }


        public async Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<Guid> Ids, bool trackChanges) =>
            await FindByCondition(x => Ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public  async Task<Word> GetAsync(Guid wordId, bool trackChanges) =>
            await FindByCondition(x => x.Id.Equals(wordId), trackChanges)
            .SingleOrDefaultAsync();

    }
}
