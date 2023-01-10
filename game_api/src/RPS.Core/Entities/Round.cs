using RPS.Core.Common;
using RPS.Core.Enums;
using RPS.Core.Services;

namespace RPS.Core.Entities
{
    public class Round : BaseEntity
    {
        public Game Game { get; set; }
        public int RoundNumber { get; set; }
        public SystemState State { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Player? Winner
        {
            get
            {
                foreach(var player in Players)
                {
                    if (Players.Where(p => p != player && p.Gesture != null).All(p => GestureService.Beat((Gesture)player.Gesture, (Gesture)p.Gesture)))
                    {
                        return player;
                    }

                }
                return null;

            }
        }
        public Boolean HasMaximumAllowedPlayers
        {
            get
            {
                return this.Players.Count == 2;
            }
        }
        public Boolean AllPlayersHaveMadeGestures
        {
            get
            {
                return this.Players.Count(p => p.Gesture != null) == 2;
            }
        }
    }
}
