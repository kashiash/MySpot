namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException :CustomException
{
    public EmptyLicensePlateException() : base("Licence Plate is empty")
    {
        
    }
}