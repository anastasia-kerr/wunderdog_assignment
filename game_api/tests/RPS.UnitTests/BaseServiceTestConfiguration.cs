using NSubstitute;
using RPS.Application.Services;

namespace RPS.UnitTests.Services;

public class BaseServiceTestConfiguration
{
    protected readonly ISystemService _systemService;

    protected BaseServiceTestConfiguration()
    {

        _systemService = Substitute.For<ISystemService>();
    }
}
