using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishTrainer.Repositories.Interfaces;

namespace EnglishTrainer.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IExampleRepository Example { get; }
        IWordRepository Word { get; }
        IIrregularVerbRepository IrregularVerb { get; }
        Task SaveAsync();
    }
}
