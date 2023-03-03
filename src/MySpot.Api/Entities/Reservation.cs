using MySpot.api.Exceptions;

namespace myspot.api.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid ParkinSpotId { get; private set; }
    public string EmployeeName { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(Guid id,Guid ParkinSpotId, string employeeName,  string licensePlate, DateTime date,
        ReservationDto reservationDto)
    {
        Id = id;
        ParkinSpotId = reservationDto.ParkingSpotId;
        EmployeeName = employeeName;
    
        LicensePlate = licensePlate;
        Date = date;
    }

    public void ChangeLicencePlate(string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate))
        {
            throw new EmptyLicensePlateException();
        }

        LicensePlate = licensePlate;
    }
}