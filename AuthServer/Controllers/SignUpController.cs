using AuthServer.Models;
using AuthServer.Repo;
using AuthServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class SignUpController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public SignUpController( IUserRepository userRepository, 
            IUserService userService )
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpPost]
        [Route("{user_id}")]
        public User CreateUser( string user_id, [FromBody]User userDto  )
        {
            var user = new User( ) { UserId = user_id, Password = userDto.Password };
            var newUser = _userRepository.CreateUser( user_id, user );

            return newUser;
        }
    }
}
