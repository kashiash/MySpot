using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace myspot.api.Controllers;

[ApiController]
[Route(template:"reservations")]
public class ReservationsController : ControllerBase
{
    private static int _id = 1;
    private static readonly List<Reservation> Reservations = new();
    private readonly List<string> parkingSpotNames = new()
    {
        "P1", "P2","P3","P4","P5"
    };

    [HttpGet( "{id:int}")]

    public ActionResult<Reservation> Get(int id)
    {
       var reservation =  Reservations.SingleOrDefault(x => x.Id == id);
       if (reservation is null)
       {
           HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
           return default;
       }

       return reservation;
    } 
    
    
    [HttpGet]
    public IEnumerable<Reservation> GetAll() => Reservations;

    // public int? Create(Reservation reservation)
    // {
    //     var now = DateTime.UtcNow.Date;
    //     var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
    //     var reamingDays = 7 - pastDays;
    //     
    // }

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        if (parkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return BadRequest();
        }

        reservation.Date = DateTime.UtcNow.AddDays(1).Date;

        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            return BadRequest();
        }
        
        
        
        reservation.Id = _id;
        Reservations.Add(reservation);
        _id++;
    }

}