using EnglishTrainer.Contracts;
using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;

namespace EnglishTrainer.Services
{
    public class ServiceManager : IServiceManager
    {
        private EFContext _dbContext;
        private IWordService _wordService;
        private IExampleService _exampleService;
        private IIrregularVerbService _irregularVerbService;

        public ServiceManager(EFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IWordService Word
        {
            get 
            { 
                if ( _wordService == null )
                    _wordService = new WordService(_dbContext);

                return _wordService;
            }
        }

        public IExampleService Example
        {
            get
            {
                if (_exampleService == null)
                    _exampleService = new ExampleService(_dbContext);

                return _exampleService;
            }
        } 
        
        public IIrregularVerbService IrregularVerb
        {
            get
            {
                if (_irregularVerbService == null)
                    _irregularVerbService = new IrregularVerbService(_dbContext);

                return _irregularVerbService;
            }
        }

        public void Save() => _dbContext.SaveChanges();
    }
}
