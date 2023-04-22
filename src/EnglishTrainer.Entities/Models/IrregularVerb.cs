using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities.Models
{
    public class IrregularVerb
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("infinitive")]
        [MaxLength(50)]
        public string Infinitive { get; set; }

        [Column("past_simple")]
        [MaxLength(50)]
        public string PastSimple { get; set; }

        [Column("past_participle")]
        [MaxLength(50)]
        public string PastParticiple { get; set; }

        [Column("short_translate")]
        [MaxLength(50)]
        public string ShortTranslate { get; set; }
    }
}
