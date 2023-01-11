using RPS.DataAccess.Persistence;
using RPS.Core.Interfaces;
using RPS.Core.Entities;

namespace RPS.DataAccess.Repositories;

public class TaskRepository : BaseRepository<SystemTask>, ITaskRepository
{
    public TaskRepository(DatabaseContext context) : base(context) { }
}
