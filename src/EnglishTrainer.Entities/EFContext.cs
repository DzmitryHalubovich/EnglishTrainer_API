using EnglishTrainer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options) : base(options) { }

        public DbSet<IrregularVerb> IrregularVerbs { get; set; }
        public DbSet<Word> Dictionary { get; set; }
        public DbSet<Example> Examples { get; set; }
    }
}
