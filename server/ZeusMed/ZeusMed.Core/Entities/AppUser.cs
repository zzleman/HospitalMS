
using Microsoft.AspNetCore.Identity;

namespace ZeusMed.Core.Entities;

public class AppUser : IdentityUser
{
    public string? Fullname { get; set; }
}

