using AutoMapper;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.Plugins.InMemory;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Fighter, Fighter>()
            .ForPath(
                dest => dest.FighterInfo.Stance,
                opt => opt.MapFrom(src => src.FighterInfo.Stance)
            )
            .ForMember(dest => dest.Perks, opt => opt.Ignore())
            .ForMember(dest => dest.TopMoves, opt => opt.MapFrom(src => src.TopMoves))
            .AfterMap(
                (src, dest) =>
                {
                    // Manually copy Perks and Divisions after mapping
                    dest.Perks = new List<PerksEnum>(src.Perks);
                    dest.Division = src.Division;
                }
            );
    }
}
