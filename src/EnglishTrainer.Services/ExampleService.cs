using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.Services
{
    public class ExampleService : ServiceBase<Example>, IExampleService
    {
        public ExampleService(EFContext dbContext) : base(dbContext) { }

    }
}
