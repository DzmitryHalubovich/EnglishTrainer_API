using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.DTO.Update
{
    public record ExampleUpdateDTO
    (
        string EnglishSentence,
        string? RussianSentence);
    
}
