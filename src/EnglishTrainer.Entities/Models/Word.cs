using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.Models
{
    public class Word
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "The word name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum lenth for the Name is 50 characters.")]
        public string Name { get; set; }

        [Column("translations")]
        [MaxLength(256, ErrorMessage = "Maximum lenth for the Translations is 256 characters.")]
        public string? Translations { get; set; }

        [Column("description")]
        [MaxLength(512, ErrorMessage = "Maximum lenth for the Description is 512 characters.")]
        public string? Description { get; set; }
        //[Column("created")]
        //public DateTime Created { get; set; }
        //[Column("modified")]
        //public DateTime Modified { get; set; }


        public ICollection<Example> Examples { get; set; }
    }
}
