using EnglishTrainer.Entities.DTO.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.DTO.Update
{
    public record WordUpdateDTO
        (
            string Name,
            string? Translations,
            string? Description,
            IEnumerable<ExampleCreateDTO>? Examples
        );

}
