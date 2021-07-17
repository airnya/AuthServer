using AuthServer.Exceptions;
using AuthServer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public interface ILoginService 
    {
        bool IsAuthorized( string encoded, string userId = null );
    };

    public class UserService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public UserService( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public bool IsAuthorized( string encoded, string userId = null )
        {
            if ( string.IsNullOrEmpty( encoded ) )
                throw new AuthException("Authentication Faild");

            var credentialBytes = Convert.FromBase64String( encoded );
            var credentials = Encoding.UTF8.GetString( credentialBytes ).Split (new[] { ':' }, 2 );
            var ( login, password ) = ( credentials[0], credentials[1] );

            if ( string.IsNullOrEmpty( login ) || string.IsNullOrEmpty( password ) )
                throw new AuthException( "Authentication Faild" );

            if ( !string.IsNullOrEmpty( userId ) && userId != login )
                throw new AuthException( "No Permission for Update" );

            var userExist = _userRepository.IsUserExist( login , password );

            return userExist ? true : throw new AuthException( "Not authorized" );
        }
    }
}
