using Microsoft.AspNetCore.Routing.Matching;

namespace myspot.api.Commands;

public record CreateReservation(Guid ParkingSpotId, Guid ReservationId,string EmployeeName,string LicensePlate,DateTime Date);