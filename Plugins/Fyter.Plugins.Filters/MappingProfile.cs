using AutoMapper;
using Fyter.CoreBusiness.Filters;

namespace Fyter.Plugins.Filters;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FighterStandUpFilter, FighterStandUpFilter>();
        CreateMap<FighterGrapplingFilter, FighterGrapplingFilter>();
        CreateMap<FighterHealthFilter, FighterHealthFilter>();
    }
}
