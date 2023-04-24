using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Services
{
    public class WordService : ServiceBase<Word>, IWordService
    {
        public WordService(EFContext dbContext) : base(dbContext) { }


    }
}
