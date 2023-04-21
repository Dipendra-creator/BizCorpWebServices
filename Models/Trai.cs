using System;
using System.Collections.Generic;

namespace BizCorp.Models;

public partial class Trai
{
    public int TraiId { get; set; }

    public string PNumber { get; set; } = null!;

    public string Month { get; set; } = null!;

    public string Year { get; set; } = null!;
}
