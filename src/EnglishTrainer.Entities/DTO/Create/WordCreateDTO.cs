using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.DTO.Create
{
    //public class WordCreateDTO
    //{
    //    public string Name { get; set; } 
    //    public string? Translations { get; set; }
    //    public string? Description { get; set; }

    //    public IEnumerable<ExampleCreateDTO>? Examples { get; set; }
    //}

    public record WordCreateDTO
        (
            string Name,
            string? Translations,
            string? Description,
            IEnumerable<ExampleCreateDTO>? Examples
        );

}
