using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class MovieCast
{
    public int MovieCastId { get; set; }

    public int CastMemberId { get; set; }

    public int PositionId { get; set; }

    public int MovieId { get; set; }

    public virtual CastMember CastMember { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
