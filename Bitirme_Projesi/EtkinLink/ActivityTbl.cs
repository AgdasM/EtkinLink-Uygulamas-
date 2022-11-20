using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class ActivityTbl
{
    public int ActivityId { get; set; }

    public string? ActivityName { get; set; }

    public DateTime? ActivityDate { get; set; }

    public DateTime? ActivityDeadLine { get; set; }

    public string? Explanation { get; set; }

    public int CityId { get; set; }

    public string? Address { get; set; }

    public int? Quota { get; set; }

    public bool? Ticket { get; set; }

    public int CategoryId { get; set; }

    public decimal? TicketPrice { get; set; }

    public bool? Approved { get; set; }

    public virtual CategoryTbl Category { get; set; } = null!;

    public virtual CityTbl City { get; set; } = null!;

    public virtual ICollection<DeleteMessageTbl> DeleteMessageTbls { get; } = new List<DeleteMessageTbl>();

    public virtual ICollection<ParticipantsTbl> ParticipantsTbls { get; } = new List<ParticipantsTbl>();
}
