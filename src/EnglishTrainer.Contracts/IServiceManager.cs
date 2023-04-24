using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishTrainer.Contracts.EntitiesServices;

namespace EnglishTrainer.Contracts
{
    public interface IServiceManager
    {
        IExampleService Example { get; }
        IWordService Word { get; }
        IIrregularVerbService IrregularVerb { get; }
        void Save();
    }
}
