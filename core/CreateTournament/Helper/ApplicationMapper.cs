using AutoMapper;
using CreateTournament.DTOs;
using CreateTournament.DTOs.Auth;
using CreateTournament.Models;

namespace CreateTournament.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<RegisterDTO, User>().ReverseMap();
            /*.ForMember(u => u.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(u => u.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.Phones, opt => opt.MapFrom(src => src.Phones))*/
            CreateMap<FormatType, FormatTypeDTO>().ReverseMap();
            CreateMap<SportType, SportTypeDTO>().ReverseMap();
            CreateMap<Tournament, TournamentDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<Match, MatchDTO>().ReverseMap();
            CreateMap<MatchResult, MatchResultDTO>().ReverseMap();
            CreateMap<PlayerStats, PlayerStatsDTO>().ReverseMap();
            CreateMap<PlayerStats, PlayerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Player.Name))
               .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Player.TeamId))
               .ForMember(dest => dest.ImagePlayer, opt => opt.MapFrom(src => src.Player.ImagePlayer))
            .ReverseMap();
        }
    }
}
