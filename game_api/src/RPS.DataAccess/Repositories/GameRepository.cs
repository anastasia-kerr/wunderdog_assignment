using RPS.DataAccess.Persistence;
using RPS.Core.Entities;
using RPS.Core.Interfaces;
using RPS.Core.Exceptions;
namespace RPS.DataAccess.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(DatabaseContext context) : base(context) { }
}
