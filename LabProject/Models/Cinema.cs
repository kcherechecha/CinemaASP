using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string CinemaName { get; set; }

    public string CinemaAddress { get; set; }

    public virtual ICollection<Hall> Halls { get; } = new List<Hall>();
}
