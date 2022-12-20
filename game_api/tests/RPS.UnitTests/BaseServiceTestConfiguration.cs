using AutoMapper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using RPS.Application.MappingProfiles;
using RPS.Core.Interfaces;

namespace RPS.UnitTests.Services;

public class BaseServiceTestConfiguration
{
    protected readonly IConfiguration Configuration;
    protected readonly IMapper Mapper;
    protected readonly IGameRepository _gameRepository;

    protected BaseServiceTestConfiguration()
    {
        Mapper = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(GameProfile)); }).CreateMapper();

        _gameRepository = Substitute.For<IGameRepository>();
    }
}
