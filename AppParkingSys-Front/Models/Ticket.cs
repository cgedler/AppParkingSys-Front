
namespace AppParkingSys_Front.Models
{
    public class Ticket
    {
        public int? Id { get; set; }
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public Ticket() { }
        public Ticket(int? id, DateTime entryTime, DateTime? exitTime)
        {
            Id = id;
            EntryTime = entryTime;
            ExitTime = exitTime;
        }
    }
}
