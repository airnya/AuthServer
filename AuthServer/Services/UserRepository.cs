using AuthServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public interface IUserRepository
    {
        User GetUser( string user_id );
        User CreateUser( string userId, User user );
        void UpdateUser( string userId, User user );
        void DeleteUser( int id );
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository( )
        {
            UserList = new Dictionary<string, User>( );
        }

        private Dictionary<string, User> UserList { get; set; }

        public User CreateUser( string userId, User user )
        {
            if ( user.NickName == null )
                user.NickName = userId;

            if ( UserList.ContainsKey( userId ) )
                return null;

            UserList.Add( user.UserId, user );
            return UserList.GetValueOrDefault( userId );
        }

        public void DeleteUser( int id )
        {
            throw new NotImplementedException();
        }

        public User GetUser( string user_id )
        {
            throw new NotImplementedException();
        }

        public void UpdateUser( string userId, User user )
        {
            if ( UserList.TryGetValue( userId, out User user1 ) )
            {
                user1 = user;
            };
        }
    }
}
