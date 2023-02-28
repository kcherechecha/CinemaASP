using System;
using System.Collections.Generic;

namespace LabProject.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<MovieGenre> MovieGenres { get; } = new List<MovieGenre>();
}
