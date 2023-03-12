using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Hall
{
    public int HallId { get; set; }

    public string HallName { get; set; }

    public int HallCapacity { get; set; }

    public int CinemaId { get; set; }

    public virtual Cinema Cinema { get; set; }

    public virtual ICollection<Session> Sessions { get; } = new List<Session>();
}
