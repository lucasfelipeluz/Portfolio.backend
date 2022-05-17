using Microsoft.AspNetCore.Identity;
using Portfolio.Domain.Enum;
using System.Collections;
using System.Collections.Generic;

namespace Portfolio.Domain.Identity
{
    public class User: IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Funcao Funcao { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
