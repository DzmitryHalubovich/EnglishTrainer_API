using EnglishTrainer.Entities.Models;
using EnglishTrainer.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Repositories.Interfaces
{
    public interface IIrregularVerbRepository
    {
        Task<IEnumerable<IrregularVerb>> GetVerbsAsync(bool trackChanges);
    }
}
