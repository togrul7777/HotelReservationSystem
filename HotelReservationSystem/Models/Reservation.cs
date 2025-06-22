namespace HotelReservationSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }


        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
