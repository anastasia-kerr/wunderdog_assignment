namespace RPS.Core.Exceptions
{
    [Serializable]
    public class GameIsNotCompleteException : Exception
    {
        public GameIsNotCompleteException() { }
        public GameIsNotCompleteException(int id) : base($"Game with id {id} is not yet complete.") { }
    }
}
