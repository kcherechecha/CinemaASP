using Microsoft.AspNetCore.Identity;
namespace LabProject.Models;

public class User : IdentityUser
{
    public string Username { get; set; }
    public string AdminName { get; set; }
}