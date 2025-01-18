
namespace AppParkingSys_Front.Models
{
    public class Ticket
    {
        public int? Id { get; set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public Ticket() { }
    }
}
