namespace ePizza.Application.CustomExceptions
{
    public class UserNotFoundException : Exception
    {

        public UserNotFoundException(string errorMessage): base(errorMessage)
        {
            
        }
    }

}
