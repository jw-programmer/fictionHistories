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
                .ForMember(dest => dest.AuthorUsername, map =>{
                    map.MapFrom(src => src.Author.Username);
                });
        }
    }
}