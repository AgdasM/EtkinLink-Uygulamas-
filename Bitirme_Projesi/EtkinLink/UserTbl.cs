using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class UserTbl
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<ParticipantsTbl> ParticipantsTbls { get; } = new List<ParticipantsTbl>();
}
