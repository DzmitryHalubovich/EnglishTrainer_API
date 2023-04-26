using AutoMapper;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.API.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Word, WordDTO>();

            CreateMap<Example, ExampleDTO>();

            CreateMap<WordCreateDTO, Word>();
        }

    }
}
