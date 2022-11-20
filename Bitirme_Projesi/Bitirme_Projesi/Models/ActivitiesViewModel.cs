namespace Bitirme_Projesi.Models
{
    public class ActivitiesViewModel
    {
        public int? UserID { get; set; }
        public int ActivityID { get; set; }
        public string? ActivityName { get; set; }

        public string ActivityDate { get; set; }
        public string ActivityDeadLine { get; set; }
        public string? Explanation { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? Quota { get; set; }
        public string? Ticket { get; set; }
        public string? Category { get; set; }
        public decimal? TicketPrice { get; set; }
        public bool? Approved { get; set; }
    }
}
