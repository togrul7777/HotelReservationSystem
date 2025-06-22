using Microsoft.AspNetCore.Identity;
namespace HotelReservationSystem.Models

{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; }
    }
}