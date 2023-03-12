using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public string SessionNumber { get; set; }

    public DateTime SessionDateTime { get; set; }

    public int HallId { get; set; }

    public int MovieId { get; set; }

    public int StatusId { get; set; }

    public virtual Hall Hall { get; set; }

    public virtual Movie Movie { get; set; }

    public virtual Status Status { get; set; }
}
