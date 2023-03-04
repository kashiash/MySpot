namespace MySpot.Api.Exceptions;

public class ParkingSpotAlreadyReservedException:CustomException
{
    public string SpotName{ get; }
    public DateTime ReservationDate{ get; }

    public ParkingSpotAlreadyReservedException(string spotName, DateTime reservationDate) 
        : base($"Parking spot d : {spotName} is already reserved at {reservationDate:d}")
    {
        SpotName = spotName;
        ReservationDate = reservationDate;
    }
}