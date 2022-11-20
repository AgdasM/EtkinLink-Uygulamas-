using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class ParticipantsTbl
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ActivityId { get; set; }

    public virtual ActivityTbl? Activity { get; set; }

    public virtual UserTbl? User { get; set; }
}
