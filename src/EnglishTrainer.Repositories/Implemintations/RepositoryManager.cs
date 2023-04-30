using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;

namespace EnglishTrainer.Repositories.Implemintations
{
    public class RepositoryManager : IRepositoryManager
    {
        private EFContext _dbContext;
        private IWordRepository _wordRepository;
        private IExampleRepository _exampleRepository;
        private IIrregularVerbRepository _irregularVerbRepository;

        public RepositoryManager(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IWordRepository Word
        {
            get
            {
                if (_wordRepository == null)
                    _wordRepository = new WordRepository(_dbContext);

                return _wordRepository;
            }
        }

        public IExampleRepository Example
        {
            get
            {
                if (_exampleRepository == null)
                    _exampleRepository = new ExampleRepository(_dbContext);

                return _exampleRepository;
            }
        }

        public IIrregularVerbRepository IrregularVerb
        {
            get
            {
                if (_irregularVerbRepository == null)
                    _irregularVerbRepository = new IrregularVerbRepository(_dbContext);

                return _irregularVerbRepository;
            }
        }

        public void SaveAsync() => _dbContext.SaveChangesAsync();
    }
}
