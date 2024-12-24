namespace Taboo.Exceptions.Game
{
    public class GameAlreadyOverException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status409Conflict;
        public string ErrorMessage { get; }
        public GameAlreadyOverException()
        {
            ErrorMessage = "Game already over!";
        }
        public GameAlreadyOverException(string message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}