using AutoMapper;
using TVSeriesAPI.Models;
using TVSeriesAPI.Models.DTOs;

namespace TVSeriesAPI.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>().ReverseMap()
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.Location))
                .ForMember(dest => dest.Episodes, opts => opts.MapFrom(src => src.Episodes));
        }
    }
}
