using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    [Required(ErrorMessage = "Назва фільму обов'язкова")]
    [Display(Name = "Назва фільму")]
    public string MovieName { get; set; }

    [Required(ErrorMessage = "Тривалість фільму обов'язкова")]
    [Display(Name = "Тривалість")]
    public int MovieDuration { get; set; }

    [Display(Name = "Рейтинг")]
    public int? MovieRating { get; set; }

    [Required(ErrorMessage = "Дата виходу обов'язкова")]
    [Display(Name = "Дата виходу")]
    public DateTime MovieReleaseDate { get; set; }

    public virtual ICollection<MovieCast> MovieCasts { get; } = new List<MovieCast>();

    public virtual ICollection<MovieGenre> MovieGenres { get; } = new List<MovieGenre>();

    public virtual ICollection<Session> Sessions { get; } = new List<Session>();
}
