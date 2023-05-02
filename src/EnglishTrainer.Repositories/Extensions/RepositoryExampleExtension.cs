using EnglishTrainer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Repositories.Extensions
{
    public static class RepositoryExampleExtension
    {
        public static IQueryable<Example> Search(this IQueryable<Example> examples, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return examples;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return examples.Where(e => e.EnglishSentence.ToLower().Contains(lowerCaseTerm));
        }
    }
}
