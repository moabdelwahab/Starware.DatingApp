using AutoMapper;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.DTOs.Users;

namespace Starware.DatingApp.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, UserDto>();
            
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.PhotoUrl ,
                opt=>opt.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url ));

           CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderPhotoUrl, options => options
                   .MapFrom(source => source.Sender.Photos.FirstOrDefault(photo => photo.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, options => options
                    .MapFrom(source => source.Recipient.Photos.FirstOrDefault(photo => photo.IsMain).Url));


            CreateMap<MemberDto, AppUser>()
                 .ForMember(dest => dest.BirthDate, options => options.Ignore())
                 .ForMember(dest => dest.Photos, options => options.Ignore())
                 .ForMember(dest => dest.PasswordHash, options => options.Ignore())
                 .AfterMap((s, d) => {
                     });






        }
    }
}
