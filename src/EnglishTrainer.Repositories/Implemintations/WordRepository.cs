using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Word>> GetAllAsync(bool trackChanges) =>
             await FindAll(trackChanges)
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<Guid> Ids, bool trackChanges) =>
            await FindByCondition(x => Ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public  async Task<Word> GetAsync(Guid wordId, bool trackChanges) =>
            await FindByCondition(x => x.Id.Equals(wordId), trackChanges)
            .SingleOrDefaultAsync();

    }
}
