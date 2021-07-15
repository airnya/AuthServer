using AuthServer.Exceptions;
using AuthServer.Models;
using AuthServer.Repo;
using AuthServer.Services;
using Microsoft.AspNetCore.Http;
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
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public UsersController( IUserRepository userRepository, IUserService userService )
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        [Route( "{user_id}" )]
        public User Get( string user_id )
            => _userRepository.GetUser( user_id );

        [HttpPatch]
        [Route( "{user_id}" )]
        public User PatchUser( [FromHeader] string login, [FromHeader] string password, 
            string user_id, [FromBody] User userDto )
        {
            _userService.IsAuthorized( login, password );

            var user = _userRepository.GetUser( user_id );
            user = userDto; // method to convert userDto = user
            return user;
        }
    }
}
