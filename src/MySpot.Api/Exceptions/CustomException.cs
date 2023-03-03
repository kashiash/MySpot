namespace MySpot.api.Exceptions;

public abstract class CustomException: Exception
{
    public CustomException(string message) : base (message)
    {
        
    }
}