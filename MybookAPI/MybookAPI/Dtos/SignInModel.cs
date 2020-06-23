using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MybookAPI.Dtos
{//DTOs control and regulate the data asked by the user during testing.
    public class SignInModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
    }
}
