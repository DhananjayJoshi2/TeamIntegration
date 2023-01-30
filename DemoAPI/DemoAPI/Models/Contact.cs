namespace DemoAPI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public long MobileNO { get; set; }
        public string Address { get; set; }
    }
}
