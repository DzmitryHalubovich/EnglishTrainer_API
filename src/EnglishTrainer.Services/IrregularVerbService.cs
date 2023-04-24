using EnglishTrainer.Contracts.EntitiesServices;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Services
{
    public class IrregularVerbService : ServiceBase<IrregularVerb>, IIrregularVerbService
    {
        public IrregularVerbService(EFContext dbContext) : base(dbContext) { }
    }
}
