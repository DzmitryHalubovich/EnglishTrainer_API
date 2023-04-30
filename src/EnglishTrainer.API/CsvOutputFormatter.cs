using EnglishTrainer.Entities.DTO.Read;
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
            if (typeof(WordReadDTO).IsAssignableFrom(type) ||
                typeof(IEnumerable<WordReadDTO>).IsAssignableFrom(type))
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

            if (context.Object is IEnumerable<WordReadDTO>)
            {
                foreach (var word in (IEnumerable<WordReadDTO>)context.Object)
                {
                    FormatCsv(buffer, word);
                }
            }
            else
            {
                FormatCsv(buffer, (WordReadDTO)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        //Формат ответа
        private static void FormatCsv(StringBuilder buffer, WordReadDTO word)
        {
            buffer.AppendLine($"{word.Id}, \"{word.Name}\", \"{word.Translations}\"");
        }
    }
}
