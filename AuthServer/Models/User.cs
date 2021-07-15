using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Comment { get; set; }
    }
}
