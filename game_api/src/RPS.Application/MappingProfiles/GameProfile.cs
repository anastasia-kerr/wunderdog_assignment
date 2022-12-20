using AutoMapper;
using RPS.Application.Models.Game;
using RPS.Core.Entities;

namespace RPS.Application.MappingProfiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<CreateGameModel, Game>()
               .ForMember(ti => ti.CreatedBy, ti => ti.MapFrom(cti => cti.Nickname));
    }}
