using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Services
{
    public class WordService : ServiceBase<Word>, IWordService
    {
        public WordService(EFContext dbContext) : base(dbContext) { }

        public void CreateWord(Word word) => Create(word);

        public IEnumerable<Word> GetAll(bool trackChanges) => 
            FindAll(trackChanges)
            .OrderBy(c=>c.Name)
            .ToList();

        public Word GetWord(Guid wordId, bool trackChanges) =>
            FindByCondition(w => w.Id.Equals(wordId), trackChanges)
            .SingleOrDefault();
    }
}
