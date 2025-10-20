namespace E_learning_platform.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message, int statusCode) : base(message, StatusCodes.Status409Conflict)
        {
        }
    }
}
