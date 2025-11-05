using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
      public string? Name { get; set; }
    }
}