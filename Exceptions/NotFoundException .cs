namespace E_learning_platform.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string resourceName, long id)
            : base($"{resourceName} với ID {id} không tồn tại", StatusCodes.Status404NotFound)
        {
        }

        public NotFoundException(string message)
            : base(message, StatusCodes.Status404NotFound)
        {
        }
    }
}