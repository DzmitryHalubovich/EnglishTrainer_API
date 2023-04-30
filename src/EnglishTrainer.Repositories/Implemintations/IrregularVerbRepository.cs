using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Entities;
using EnglishTrainer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Repositories.Implemintations
{
    public class IrregularVerbRepository : RepositoryBase<IrregularVerb>, IIrregularVerbRepository
    {
        public IrregularVerbRepository(EFContext dbContext) : base(dbContext) { }
    }
}
