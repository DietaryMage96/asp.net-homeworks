using AutoMapper;
using Bll.DTO;
using BLL.DTO;
using less10.Contracts;
using less10.DTOs;

namespace less10.MapperProfiles
{
    public class RequestResponceProfile : Profile
    {
        public RequestResponceProfile()
        {
            CreateMap<CreateMovieRequest, CreateMovieDto>();
            CreateMap<UpdateMovieRequest, UpdateMovieDto>();
            CreateMap<MovieDto, MovieResponse>();

            CreateMap<IdentityRegisterRequest, RegisterUserDto>();
            CreateMap<LoginRequest, LoginUserDto>();
            CreateMap<UserDto, UserResponse>();

            CreateMap<CreateMovieCommentRequest, CreateMovieCommentDto>();
            CreateMap<UpdateMovieCommentRequest, UpdateMovieCommentDto>();
            CreateMap<MovieCommentDto, MovieCommentResponse>();
        }
    }
}
