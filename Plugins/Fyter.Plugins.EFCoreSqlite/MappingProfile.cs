using AutoMapper;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.Plugins.EFCoreSqlite;

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
            .ForMember(dest => dest.IsOutdated, opt => opt.MapFrom(src => src.IsOutdated))
            .ForMember(dest => dest.OutdatedStats, opt => opt.MapFrom(src => src.OutdatedStats))
            .AfterMap(
                (src, dest) =>
                {
                    dest.Perks = new List<PerksEnum>(src.Perks);
                    dest.Division = src.Division;
                }
            );
    }
}
