using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.Models
{
    public class Example
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("english_sentence")]
        [Required(ErrorMessage = "English sentece is a required field")]
        [MaxLength(256, ErrorMessage = "Maximum lenth for the English sentece is 256 characters.")]
        public string EnglishSentence { get; set; }

        [Column("russian_sentence")]
        [MaxLength(256, ErrorMessage = "Maximum lenth for the Russian sentece is 256 characters.")]
        public string? RussianSentence { get; set; }

        [Column("word_id")]
        [ForeignKey(nameof(Word))]
        public Guid WordId { get; set; }
        public Word Word { get; set; }
    }
}
