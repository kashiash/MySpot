using Microsoft.AspNetCore.Mvc;
using myspot.api.Commands;
using myspot.api.Entities;
using myspot.api.Serwajsy;

namespace myspot.api.Controllers;

[ApiController]
[Route(template:"reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationSerwajs _serwajs = new();

    [HttpGet( "{id:guid}")]

    public ActionResult<Reservation> Get(Guid id)
    {
        var reservation = _serwajs.Get(id);
       if (reservation is null)
       {
           return NotFound();
       }

       return Ok(reservation);
    } 
    
    
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_serwajs.GetAllWeekly());

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = _serwajs.Create(command with { ReservationId = Guid.NewGuid() });

        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id} , null);
}
    [HttpPut("{id:int}")]
    public ActionResult Put(Guid id, Reservation reservation)
    {
        reservation.Id = id;
        if (_serwajs.Update(reservation))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        if (_serwajs.Delete(id))
        {
            return NoContent();
        }
        return NotFound();
    }
}