namespace PierwszaAplikacjaMVC.Models
{
    public class GuestResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool? WillAttend { get; set; }
    }
}