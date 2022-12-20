using RPS.Core.Common;
using RPS.Core.Enums;

namespace RPS.Core.Entities
{
    public class Game : BaseEntity
    {
        public List<Round> Rounds { get; set; } = new List<Round>();
        public string CreatedBy { get; set; }

        public Round CurrentRound
        {
            get
            {
                return Rounds.OrderBy(r => r.RoundNumber).FirstOrDefault(r => r.State != RoundState.Completed);
            }
        }
        public Boolean IsGameComplete {
            get
            {
                return this.CurrentRound == null;
            } 
        }
    }
}
