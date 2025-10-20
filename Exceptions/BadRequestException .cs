namespace E_learning_platform.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, int statusCode) : base(message, StatusCodes.Status400BadRequest)
        {


        }
    }
}
