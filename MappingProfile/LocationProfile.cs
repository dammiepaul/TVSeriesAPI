using AutoMapper;
using TVSeriesAPI.Models;
using TVSeriesAPI.Models.DTOs;

namespace TVSeriesAPI.MappingProfiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
