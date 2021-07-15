using AuthServer.Exceptions;
using AuthServer.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public interface IUserService 
    {
        bool IsAuthorized( string login, string password );
    };

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public bool IsAuthorized( string login, string password )
        {
            if ( string.IsNullOrEmpty( login ) || string.IsNullOrEmpty( password ) )
                throw new AuthException( "Login and password is required" );

            var userExist = _userRepository.IsUserExist( login, password );

            return userExist ? true : throw new AuthException( "Not authorized" );
        }
    }
}
