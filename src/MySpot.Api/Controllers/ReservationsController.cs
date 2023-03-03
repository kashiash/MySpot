using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using myspot.api.Serwajsy;

namespace myspot.api.Controllers;

[ApiController]
[Route(template:"reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationSerwajs _serwajs = new();

    [HttpGet( "{id:int}")]

    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _serwajs.Get(id);
       if (reservation is null)
       {
           return NotFound();
       }

       return Ok(reservation);
    } 
    
    
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_serwajs.GetAll());

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        var id = _serwajs.Create(reservation);

        if (id is null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Get),new {id = reservation.Id},null);
    }
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        reservation.Id = id;
        if (_serwajs.Update(reservation))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        if (_serwajs.Delete(id))
        {
            return NoContent();
        }
        return NotFound();
    }
}