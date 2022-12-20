using RPS.Core.Enums;

namespace RPS.Core.Services
{
    public static class GestureService
    {
       public static bool Beat(Gesture first, Gesture second)
        {
            var rules = new List<(Gesture value, Gesture beats)>()
        {
            ( Gesture.Rock, Gesture.Scissors ),
            ( Gesture.Scissors, Gesture.Paper),
            ( Gesture.Paper, Gesture.Rock )
        };
            var firstRule = rules.Find(u => u.value == first);
            return firstRule.beats == second;
        }
    };
}

