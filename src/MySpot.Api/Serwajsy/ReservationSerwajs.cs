using myspot.api.Commands;
using myspot.api.Entities;

namespace myspot.api.Serwajsy;

public class ReservationSerwajs
{

    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpot = new()
    {
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P1" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P2" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P3" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P4" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P5" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P6" ),
       new WeeklyParkingSpot(Guid.NewGuid(),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P7" ),
    };

    public Reservation Get(Guid id) =>  GetAllWeekly().SingleOrDefault(x => x.Id == id);


    public IEnumerable<Reservation> GetAllWeekly() => WeeklyParkingSpot.SelectMany(c=> c.Reservations);

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = WeeklyParkingSpot.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId,  command.ParkingSpotId,command.EmployeeName,
            command.LicensePlate, command.);
        weeklyParkingSpot.AddReservation(reservation);
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

    public bool Delete(Guid id)
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