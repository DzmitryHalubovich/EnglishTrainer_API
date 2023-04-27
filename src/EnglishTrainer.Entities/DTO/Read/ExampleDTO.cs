using EnglishTrainer.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.DTO.Read
{
    public record ExampleDTO(
        Guid Id,
        string EnglishSentence,
        string? RussianSentence);
}
