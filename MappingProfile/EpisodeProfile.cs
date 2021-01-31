using AutoMapper;
using TVSeriesAPI.Models;
using TVSeriesAPI.Models.DTOs;

namespace TVSeriesAPI.MappingProfiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDto>().ReverseMap()
                .ForMember(dest => dest.EpisodeComments.Count, opts => opts.MapFrom(src => src.NumberOfComments));
        }
    }
}
