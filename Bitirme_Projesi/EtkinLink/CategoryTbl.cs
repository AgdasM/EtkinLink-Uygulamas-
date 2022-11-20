using System;
using System.Collections.Generic;

namespace EtkinLink;

public partial class CategoryTbl
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<ActivityTbl> ActivityTbls { get; } = new List<ActivityTbl>();
}
