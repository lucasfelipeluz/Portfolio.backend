using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
    }
}
