using AutoMapper;
using Src.Dtos;
using Src.Models;

namespace Src.Mappers
{
    public class ModelsMapper: Profile
    {
        public ModelsMapper()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AuthorUserName, map =>{
                    map.MapFrom(src => src.Author.UserName);
                });
            CreateMap<NewAuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<History, HistoryDto>();
        }
    }
}