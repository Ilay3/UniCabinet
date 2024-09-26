using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Role : IdentityRole<string>
    {
        // Навигационные свойства
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
