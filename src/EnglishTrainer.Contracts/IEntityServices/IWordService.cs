using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Contracts.EntitiesServices
{
    public interface IWordService
    {

        IEnumerable<Word> GetAll(bool trackChanges);
    }
}
