using AutoMapper;
using VillaAPI.DTOs;
using VillaAPI.Entities;

namespace VillaAPI.AutoMapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDto>().ReverseMap();
        CreateMap<Villa, VillaCreateDto>().ReverseMap();
        CreateMap<Villa, VillaUpdateDto>().ReverseMap();



    }
    
}