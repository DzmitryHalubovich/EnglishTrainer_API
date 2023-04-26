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
            .OrderBy(x=>x.Name)
            .ToList();

        public IEnumerable<Word> GetByIds(IEnumerable<Guid> Ids, bool trackChanges) =>
            FindByCondition(x => Ids.Contains(x.Id), trackChanges)
            .ToList();


        public Word GetWord(Guid wordId, bool trackChanges) =>
            FindByCondition(x => x.Id.Equals(wordId), trackChanges)
            .SingleOrDefault();
    }
}
