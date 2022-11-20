using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class DeleteMessageTbl
{
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public string? Message { get; set; }

    public virtual ActivityTbl? Activity { get; set; }
}
