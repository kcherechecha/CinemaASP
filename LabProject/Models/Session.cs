using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public string SessionNumber { get; set; } = null!;

    public DateTime SessionDateTime { get; set; }

    public bool SessionState { get; set; }

    public int HallId { get; set; }

    public int MovieId { get; set; }

    public virtual Hall Hall { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
