namespace RPS.Application.Models.System
{
    public class SystemTaskModel {
        public Guid Id { get; set; }

        public string Title { get; set; }

            public string Importance { get; set; }

            public bool IsOff { get; set; }

            public DateTime? LastStopped { get; set; }
    }
    public class GetTasksResponseModel
    {
        public IList<SystemTaskModel> Tasks { get; set; }
    }
}
