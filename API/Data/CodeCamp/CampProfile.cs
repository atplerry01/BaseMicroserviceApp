using API.Data.CodeCamp.Entities;
using API.Models.CodeCamp;
using AutoMapper;

namespace API.Data.CodeCamp
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>()
              .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
              .ReverseMap();

            this.CreateMap<Talk, TalkModel>()
              .ReverseMap()
              .ForMember(t => t.Camp, opt => opt.Ignore())
              .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, SpeakerModel>()
              .ReverseMap();
        }
    }
}
