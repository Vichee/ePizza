
namespace ePizza.Application.CustomExceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string errorMessage) : base(errorMessage)
        {

        }

    }
}
