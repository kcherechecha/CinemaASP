using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieName { get; set; }

    public int MovieDuration { get; set; }

    public int? MovieRating { get; set; }

    public DateTime MovieReleaseDate { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; } = new List<MovieCast>();

    public virtual ICollection<MovieGenre> MovieGenres { get; } = new List<MovieGenre>();

    public virtual ICollection<Session> Sessions { get; } = new List<Session>();
}
