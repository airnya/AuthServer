using AuthServer.Exceptions;
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
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class CloseController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginService _loginService;

        public CloseController( IUserRepository userRepository,
            ILoginService userService )
        {
            _userRepository = userRepository;
            _loginService = userService;
        }

        [HttpPost]
        [Route( "{user_id}" )]
        public IActionResult DeleteUser( string user_id )
        {
            AuthenticationHeaderValue.TryParse( Request.Headers["Authorization"], out var authHeader );
            _loginService.IsAuthorized( authHeader?.Scheme );

            _userRepository.DeleteUser( user_id );
            return Ok( "Account and user successfully removed" );
        }
    }
}
