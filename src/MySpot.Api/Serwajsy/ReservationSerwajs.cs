using myspot.api.Commands;
using MySpot.Api.Entities;

namespace myspot.api.Serwajsy;

public class ReservationSerwajs
{

    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
    {
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P1" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P2" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P3" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P4" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P5" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000006"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P6" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000007"),DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P7" ),
    };

    public ReservationDto Get(Guid id) =>  GetAllWeekly().SingleOrDefault(x => x.Id == id);


    public IEnumerable<ReservationDto> GetAllWeekly() => WeeklyParkingSpots.SelectMany(c => c.Reservations)
        .Select(x => new ReservationDto
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            Date = x.Date,

        });

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId,  command.ParkingSpotId,command.EmployeeName,
            command.LicensePlate, command.Date);
        weeklyParkingSpot.AddReservation(reservation);
        return reservation.Id;
    }

    public bool Update(ChangeLicensePlate command)
    {
        var weeklyParkingSpot =  GetWeeklyParkingSpot(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.Date < DateTime.UtcNow)
        {
            return false;
        }

        existingReservation.ChangeLicencePlate(command.LicensePlate);
        return true;
    }


    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpot(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }
        weeklyParkingSpot.RemoveReservation(existingReservation.Id);
        return true;
    }
    
   
    
    WeeklyParkingSpot GetWeeklyParkingSpot(Guid id) => 
        WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(x=>x.Id ==id));
}