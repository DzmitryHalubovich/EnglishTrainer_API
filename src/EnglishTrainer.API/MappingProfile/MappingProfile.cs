using AutoMapper;
using EnglishTrainer.Entities.DTO;
using EnglishTrainer.Entities.Models;

namespace EnglishTrainer.API.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Word, WordDTO>();

            CreateMap<Example, ExampleDTO>();
        }

    }
}
