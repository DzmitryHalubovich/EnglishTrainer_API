using System.ComponentModel.DataAnnotations;

namespace EnglishTrainer.Entities.DTO
{
    public abstract class ExampleBaseDTO
    {
        [Required(ErrorMessage = "English sentece is a required field")]
        [MaxLength(256, ErrorMessage = "Maximum lenth for the English sentece is 256 characters.")]
        public string EnglishSentence { get; set; }

        [MaxLength(256, ErrorMessage = "Maximum lenth for the Russian sentece is 256 characters.")]
        public string? RussianSentence { get; set; }
    }
}
