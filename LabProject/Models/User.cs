using Microsoft.AspNetCore.Identity;
namespace LabProject.Models;

public class User : IdentityUser
{
    public string AdminName { get; set; }
}