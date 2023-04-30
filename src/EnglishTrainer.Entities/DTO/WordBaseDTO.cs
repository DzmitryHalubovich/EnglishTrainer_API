using EnglishTrainer.Entities.DTO.Create;
using System.ComponentModel.DataAnnotations;

namespace EnglishTrainer.Entities.DTO
{
    public abstract class WordBaseDTO
    {
        [Required(ErrorMessage = "The word name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum lenth for the Name is 50 characters.")]
        public string Name { get; set; }
        [MaxLength(256, ErrorMessage = "Maximum lenth for the Translations is 256 characters.")]
        public string? Translations { get; set; }
        [MaxLength(512, ErrorMessage = "Maximum lenth for the Description is 512 characters.")]
        public string? Description { get; set; }

        IEnumerable<ExampleCreateDTO>? Examples { get; set; }
    }
}
