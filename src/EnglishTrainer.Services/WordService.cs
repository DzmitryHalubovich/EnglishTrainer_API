using EnglishTrainer.Entities.Models;
using EnglishTrainer.Repositories.Interfaces;

namespace EnglishTrainer.Services
{
    public class WordService
    {
        private readonly IRepositoryManager _repositoryManager;
        public WordService(IRepositoryManager repositoryManager)
        {
            _repositoryManager=repositoryManager;
        }

        public bool Create(Word word)
        {
            _repositoryManager.Word.CreateWord(word);
            return true;
        }
    }
}