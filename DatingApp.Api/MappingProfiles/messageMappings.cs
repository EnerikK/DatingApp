using AutoMapper;
using DatingApp.Api.Interfaces;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.MappingProfiles;

public class messageMappings : Profile
{
    public messageMappings()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderPhotoUrl,
                opt => 
                    opt.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain)!.Url))
            .ForMember(dest => dest.RecipientPhotoUrl,
            opt =>
                opt.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain)!.Url));
        
        
    }
}