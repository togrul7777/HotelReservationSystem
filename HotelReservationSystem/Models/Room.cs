namespace HotelReservationSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capasity { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }


    }
}
