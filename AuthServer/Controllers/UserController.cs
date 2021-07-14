using AuthServer.Models;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route( "{user_id}" )]
        public HttpResponseMessage Get( string user_id )
        {
            var user = _userRepository.GetUser( user_id );
            if ( user == null )
                return new HttpResponseMessage( HttpStatusCode.NotFound) { Content = new StringContent( "No User Found" ) };
            else
                return new HttpResponseMessage( HttpStatusCode.OK ) { Content = new StringContent( user.ToString( ) ) };
        }

        //[HttpPatch]
        //[Route("{user_id}")]
        //public HttpResponseMessage PatchUser( string user_id, 
        //    [FromBody]User userDto  )
        //{
    
        //}
    }
}
