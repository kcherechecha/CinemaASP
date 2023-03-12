using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models;

public partial class Cinema
{
    public int CinemaId { get; set; }
    [Required(ErrorMessage ="Назва кінотеатру обов'язкова")]
    [Display(Name ="Назва")]
    public string CinemaName { get; set; }
    [Required(ErrorMessage = "Адреса кінотеатру обов'язкова")]
    [Display(Name ="Адреса")]
    [Remote("AddressExists", "Cinemas", HttpMethod ="POST", ErrorMessage ="За цією адресою вже існує кінотеатр")]
    public string CinemaAddress { get; set; }

    public virtual ICollection<Hall> Halls { get; } = new List<Hall>();
}
