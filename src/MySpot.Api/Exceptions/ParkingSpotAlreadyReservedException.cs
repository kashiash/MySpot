namespace MySpot.api.Exceptions;

public class ParkingSpotAlreadyReservedException:CustomException
{
    public string SpotName{ get; }

    public ParkingSpotAlreadyReservedException(string spotName, DateTime reservationDate) 
        : base($"Parking spot d : {spotName} is already reserved at {reservationDate:d}")
    {
        SpotName = spotName;
    }
}