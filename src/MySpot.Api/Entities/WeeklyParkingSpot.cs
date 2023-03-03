using MySpot.api.Exceptions;

namespace myspot.api.Entities;

public class WeeklyParkingSpot
{

    private readonly HashSet<Reservation> _reservations = new();
    public Guid Id { get; }
    public DateTime From { get; }
    public DateTime To { get; }
    public string Name { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
    {
        Id = id;
        From = from;
        To = to;
        Name = name;
    }

    public void AddReservation(Reservation reservation)
    {
      
        var isInvalidDate = reservation.Date.Date < DateTime.UtcNow.Date && reservation.Date.Date < From && reservation.Date.Date > To;
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Date);
        }
        
        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            throw new ParkingSpotAlreadyReservedException(reservation.ParkingSpotName,reservation.Date);
        }
    }
}