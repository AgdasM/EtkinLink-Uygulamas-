using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bitirme_Projesi.Models
{
    public class CreateEventViewModels
    {
        public List<CityViewModel>? City { get; set; }
        public List<CategoryViewModel>? Category { get; set; }
        public List<TicketYesNo>? Ticket { get; set; }
    }
}
