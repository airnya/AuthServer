using AuthServer.Models;
using AuthServer.Responses;
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

        public SignUpController( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("{user_id}")]
        public HttpResponseMessage CreateUser( string user_id, 
            [FromBody]User userDto  )
        {
            if ( string.IsNullOrEmpty( user_id ) || string.IsNullOrEmpty( userDto.Password ) )
                return new HttpResponseMessage( HttpStatusCode.BadRequest ) { Content = new StringContent( "required user_id and password" ) };

            var user = new User( userDto.Password );
            var newUser = _userRepository.CreateUser( user_id, user );

            if ( newUser == null )
                return new HttpResponseMessage( HttpStatusCode.BadRequest ) { Content = new StringContent( "already same user_id is used" ) };
            else
                return new HttpResponseMessage( HttpStatusCode.OK ) { Content = new StringContent( newUser.ToString( ) ) };
        }
    }
}
