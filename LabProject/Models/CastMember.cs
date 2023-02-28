using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class CastMember
{
    public int CastMemberId { get; set; }

    public string CastMemberFullName { get; set; } = null!;

    public virtual ICollection<MovieCast> MovieCasts { get; } = new List<MovieCast>();
}
