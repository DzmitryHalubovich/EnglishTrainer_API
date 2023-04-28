using AutoMapper;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.DTO.Update;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.API.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            CreateMap<Example, ExampleReadDTO>();
            CreateMap<ExampleCreateDTO, Example>();
            CreateMap<ExampleUpdateDTO, Example>().ReverseMap();

            CreateMap<Word, WordReadDTO>();
            CreateMap<WordCreateDTO, Word>();
            CreateMap<WordUpdateDTO, Word>();

        }

    }
}
