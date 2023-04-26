using EnglishTrainer.Entities.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace EnglishTrainer.API
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            //Какие типы поддерживаются
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        //Проверка можно ли сериализовать
        protected override bool CanWriteType(Type type)
        {
            if (typeof(WordDTO).IsAssignableFrom(type) ||
                typeof(IEnumerable<WordDTO>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        //Формируем ответ
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, 
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<WordDTO>)
            {
                foreach (var word in (IEnumerable<WordDTO>)context.Object)
                {
                    FormatCsv(buffer, word);
                }
            }
            else
            {
                FormatCsv(buffer, (WordDTO)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        //Формат ответа
        private static void FormatCsv(StringBuilder buffer, WordDTO word)
        {
            buffer.AppendLine($"{word.Id}, \"{word.Name}\", \"{word.Translations}\"");
        }
    }
}
