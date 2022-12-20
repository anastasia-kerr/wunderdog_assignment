namespace RPS.Core.Exceptions
{
    [Serializable]
    public class GameCompleteException : Exception
    {
        public GameCompleteException() { }
        public GameCompleteException(int id) : base($"Game with id {id} is complete.") { }
    }
}
