using MySpot.Api.Models;

namespace myspot.api.Serwajsy;

public class ReservationSerwajs
{
    private static int _id = 1;
    private static readonly List<Reservation> Reservations = new();
    private readonly List<string> parkingSpotNames = new()
    {
        "P1", "P2","P3","P4","P5"
    };

    public Reservation Get(int id) =>  Reservations.SingleOrDefault(x => x.Id == id);


    public IEnumerable<Reservation> GetAll() => Reservations;

    public int? Create(Reservation reservation)
    {
        var now = DateTime.UtcNow.Date;
        var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
        var remainingDays = 7 - pastDays;
        
        if (parkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            return default;
        }

        if ((reservation.Date.Date >= now && reservation.Date.Date <= now.AddDays(remainingDays)) == false)
        {
            return default;
        }

        if (string.IsNullOrEmpty(reservation.LicensePlate))
        {
            return default;
        }

        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            return default;
        }
        reservation.Id = _id;
        Reservations.Add(reservation);
        _id++;
        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation =  Reservations.SingleOrDefault(x => x.Id == reservation.Id);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.Date < DateTime.UtcNow)
        {
            return false;
        }

        existingReservation.LicensePlate = reservation.LicensePlate;
        return true;
    }

    public bool Delete(int id)
    {
        var existingReservation =  Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation is null)
        {
            return false;
        }
        Reservations.Remove(existingReservation);
        return true;
    }
}