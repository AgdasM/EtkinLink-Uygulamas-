namespace Bitirme_Projesi.Models
{
    public class ActivityCreateViewModel
    {
        public int ActivityID { get; set; }
        public string? ActivityName { get; set; }

        public DateTime? ActivityDate { get; set; }
        public DateTime? ActivityDeadLine { get; set; }
        public string? Explanation { get; set; }
        public int? Cityid { get; set; }
        public string? Address { get; set; }
        public int? Quota { get; set; }
        public bool? Ticket { get; set; }
        public int? Categoryid { get; set; }
        public decimal? TicketPrice { get; set; }
        public bool? Approved { get; set; }
    }
}
