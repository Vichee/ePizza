
namespace ePizza.Application.CustomExceptions
{
    public class InvalidAccessTokenException : Exception
    {
        public InvalidAccessTokenException(string errorMessage) : base(errorMessage)
        {

        }

    }
}
