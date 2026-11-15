using AutoMapper;
using Bll.DTO;
using BLL.DTO;
using Domain.Models;
using less10.DTOs;

namespace less10.MapperProfiles
{
    public class DtoMapperProfile : Profile
    {
        public DtoMapperProfile()
        {
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>();

            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CreateMovieCommentDto, MovieComment>();
            CreateMap<MovieComment, MovieCommentDto>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
