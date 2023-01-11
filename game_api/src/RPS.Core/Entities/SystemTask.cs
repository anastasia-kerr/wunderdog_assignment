using RPS.Core.Common;
using RPS.Core.Enums;

namespace RPS.Core.Entities
{
    public class SystemTask : BaseEntity
    {
        public string Title { get; set; }

        public Importance Importance { get; set; }

        public bool IsOff { get; set; }

        public DateTime? LastStopped { get; set; }

        public bool HasEverStopped
        {
            get
            {
                return LastStopped != null;
            }
        }
    }
}
