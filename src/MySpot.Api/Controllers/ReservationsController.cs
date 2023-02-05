using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace myspot.api.Controllers;

[ApiController]
[Route(template:"reservations")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
    }

    [HttpPost]
    public void Post(Reservation reservation)
    {
    }

}