using API.DTOs;
using API.Entities;
using API.MyExtensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    //CreateMap<AppUser, MemberDTO>(); all class mapping, following is with speicfic colum mapping which is not present in source
    CreateMap<AppUser, MemberDTO>()  // it is used for automapping a class with extra column mapping
      .ForMember(dest => dest.PhotoUrl, 
          opt => opt.MapFrom(src => src.Photos.FirstOrDefault().URL))
      .ForMember(dest => dest.Age,
          opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

    CreateMap<Photo, PhotoDTO>();
  }
}
