using EnglishTrainer.Entities.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EnglishTrainer.Repositories.Extensions
{
    public static class RepositoryWordExtension
    {
        public static IQueryable<Word> Sort(this IQueryable<Word> words, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return words.OrderBy(w => w.Name);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Word).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrEmpty(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(
                    pi => pi.Name.Equals(propertyFromQueryName, 
                    StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null) 
                    continue; 

                var direction = param.EndsWith(" desc") ? "descending" : "ascending"; 
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); 
            if (string.IsNullOrWhiteSpace(orderQuery)) 
                return words.OrderBy(e => e.Name); 
            
            return words.OrderBy(orderQuery);

        }

        public static IQueryable<Word> Search(this IQueryable<Word> examples, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return examples;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return examples.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}
