using EnglishTrainer.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EnglishTrainer.Repositories.Interfaces
{
    public interface IWordRepository
    {
        Task<IEnumerable<Word>> GetAllAsync(bool trackChanges);
        Task<Word> GetAsync(Guid wordId, bool trackChanges);
        Task<IEnumerable<Word>> GetByIdsAsync(IEnumerable<Guid> Ids,bool trackChanges);
        void CreateWord(Word word);
        void DeleteWord(Word word);
    }
}
