using EnglishTrainer.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EnglishTrainer.Contracts.EntitiesServices
{
    public interface IWordService
    {
        IEnumerable<Word> GetAll(bool trackChanges);
        IEnumerable<Word> GetByIds(IEnumerable<Guid> Ids,bool trackChanges);
        Word GetWord(Guid wordId, bool trackChanges);
        void CreateWord(Word word);
    }
}
