using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class CityTbl
{
    public int CityId { get; set; }

    public string? City { get; set; }

    public virtual ICollection<ActivityTbl> ActivityTbls { get; } = new List<ActivityTbl>();
}
