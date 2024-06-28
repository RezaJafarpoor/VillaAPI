using Microsoft.AspNetCore.Identity;

namespace VillaAPI.Entities;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    
}