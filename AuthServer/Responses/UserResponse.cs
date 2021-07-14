using AuthServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Responses
{
    public class UserResponse : BaseResponse
    {
        public UserResponse( User user, string message = null )
        {

        }

        public User User { get; set; }
    }
}
