using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

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
            x.ParkingSpotId == reservation.ParkingSpotId && x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            throw new ParkingSpotAlreadyReservedException(Name,reservation.Date);
        }
        _reservations.Add(reservation);
    }
    public bool RemoveReservation(Guid reservationId)
    {
        var reservation = Reservations.SingleOrDefault(x => x.Id == reservationId);
        if (reservation is null)
        {
            return false;
        }

    

        return _reservations.Remove(reservation);
    }
    
    public bool UpdateReservation(Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == reservation.Id);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.Date < DateTime.UtcNow)
        {
            return false;
        }

        existingReservation.ChangeLicencePlate(reservation.LicensePlate);
        return true;
    }
}