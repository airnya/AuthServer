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
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginService _loginService;
        public UsersController( IUserRepository userRepository, ILoginService userService )
        {
            _userRepository = userRepository;
            _loginService = userService;
        }

        [HttpGet]
        [Route( "{user_id}" )]
        public User Get( string user_id )
            => _userRepository.GetUser( user_id );

        [HttpPatch]
        [Route( "{user_id}" )]
        public IActionResult PatchUser( string user_id, [FromBody] User userDto )
        {
            AuthenticationHeaderValue.TryParse( Request.Headers["Authorization"], out var authHeader );
            _loginService.IsAuthorized( authHeader?.Scheme, user_id );
            var user = userDto; // TODO: method to convert userDto = user
             _userRepository.UpdateUser( user_id, user );
            
            return Ok( "User successfully updated" );
        }
    }
}
