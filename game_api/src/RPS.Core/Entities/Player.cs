using RPS.Core.Common;
using RPS.Core.Enums;

namespace RPS.Core.Entities
{
    public class Player : BaseEntity {
        public Round Round { get; set; }
        public string Nickname { get; set; }
        public string Identifier { get; set; }
        public Gesture? Gesture { get; set; }
    }
}
