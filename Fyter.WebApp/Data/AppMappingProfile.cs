using AutoMapper;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Data;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Fighter, Fighter>()
            .ForMember(dest => dest.Perks, opt => opt.Ignore())
            .ForMember(dest => dest.AlterEgos, opt => opt.Ignore())
            .ForMember(dest => dest.TopMoves, opt => opt.MapFrom(src => src.TopMoves))
            .AfterMap(
                (src, dest, context) =>
                {
                    dest.Perks = new List<PerksEnum>(src.Perks);
                    dest.Division = src.Division;
                    dest.AlterEgos = src
                        .AlterEgos?.Select(e => context.Mapper.Map<Fighter>(e))
                        .ToList();
                }
            );

        // Map StandUp
        CreateMap<StandUp, StandUp>()
            .ForMember(dest => dest.PunchSpeed, opt => opt.MapFrom(src => src.PunchSpeed))
            .ForMember(dest => dest.PunchPower, opt => opt.MapFrom(src => src.PunchPower))
            .ForMember(dest => dest.Accuracy, opt => opt.MapFrom(src => src.Accuracy))
            .ForMember(dest => dest.Blocking, opt => opt.MapFrom(src => src.Blocking))
            .ForMember(dest => dest.HeadMovement, opt => opt.MapFrom(src => src.HeadMovement))
            .ForMember(dest => dest.FootWork, opt => opt.MapFrom(src => src.FootWork))
            .ForMember(dest => dest.SwitchStance, opt => opt.MapFrom(src => src.SwitchStance))
            .ForMember(dest => dest.TakedownDefense, opt => opt.MapFrom(src => src.TakedownDefense))
            .ForMember(dest => dest.KickPower, opt => opt.MapFrom(src => src.KickPower))
            .ForMember(dest => dest.KickSpeed, opt => opt.MapFrom(src => src.KickSpeed));

        // Map Grappling
        CreateMap<Grappling, Grappling>()
            .ForMember(dest => dest.TakeDowns, opt => opt.MapFrom(src => src.TakeDowns))
            .ForMember(dest => dest.TopControl, opt => opt.MapFrom(src => src.TopControl))
            .ForMember(dest => dest.BottomControl, opt => opt.MapFrom(src => src.BottomControl))
            .ForMember(
                dest => dest.SubmissionOffense,
                opt => opt.MapFrom(src => src.SubmissionOffense)
            )
            .ForMember(
                dest => dest.SubmissionDefense,
                opt => opt.MapFrom(src => src.SubmissionDefense)
            )
            .ForMember(dest => dest.GroundStriking, opt => opt.MapFrom(src => src.GroundStriking))
            .ForMember(dest => dest.ClinchStriking, opt => opt.MapFrom(src => src.ClinchStriking))
            .ForMember(dest => dest.ClinchControl, opt => opt.MapFrom(src => src.ClinchControl));

        // Map Health
        CreateMap<Health, Health>()
            .ForMember(dest => dest.Cardio, opt => opt.MapFrom(src => src.Cardio))
            .ForMember(dest => dest.Chin, opt => opt.MapFrom(src => src.Chin))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
            .ForMember(dest => dest.Legs, opt => opt.MapFrom(src => src.Legs))
            .ForMember(dest => dest.Recovery, opt => opt.MapFrom(src => src.Recovery))
            .ForMember(dest => dest.CutResistant, opt => opt.MapFrom(src => src.CutResistant));

        // Map FighterInfo
        CreateMap<FighterInfo, FighterInfo>()
            .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.NickName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.Reach, opt => opt.MapFrom(src => src.Reach))
            .ForMember(dest => dest.Stance, opt => opt.MapFrom(src => src.Stance))
            .ForMember(dest => dest.HomeTown, opt => opt.MapFrom(src => src.HomeTown))
            .ForMember(dest => dest.FightingOutOf, opt => opt.MapFrom(src => src.FightingOutOf));

        // Map TopMoves
        CreateMap<TopMoves, TopMoves>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MoveName, opt => opt.MapFrom(src => src.MoveName))
            .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars));

        // Map Stat
        CreateMap<Stat, Stat>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars));
    }
}
