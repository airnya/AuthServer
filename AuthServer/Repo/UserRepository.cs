using AuthServer.Exceptions;
using AuthServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthServer.Repo
{
    public interface IUserRepository
    {
        User GetUser( string user_id );
        User CreateUser( string userId, User user );
        void UpdateUser( string userId, User user );
        void DeleteUser( int id );
        bool IsUserExist( string login, string password );
    }

    public class UserRepository : IUserRepository
    {
        public UserRepository( )
        {
            UserList = new Dictionary<string, User>( );
        }

        private Dictionary<string, User> UserList { get; set; }

        public bool IsUserExist( string login, string password )
        {
            var user = UserList.Values.FirstOrDefault( u => ( u.NickName == login || u.UserId == login ) && u.Password == password );
            return user != null;
        }

        public User CreateUser( string userId, User user )
        {
            if (UserList.ContainsKey( userId ) )
                throw new DBException( "User already exist" );

            if ( user.NickName == null )
                user.NickName = userId;

            UserList.Add( userId, user );
            return UserList.GetValueOrDefault( userId );
        }

        public void DeleteUser( int id )
        {
            throw new NotImplementedException();
        }

        public User GetUser( string user_id )
        {
            var user = UserList.GetValueOrDefault( user_id );
            return user ?? throw new KeyNotFoundException( "User Not Found" );
        }

        public void UpdateUser( string userId, User user )
        {
            if ( UserList.TryGetValue( userId, out User dbUser ) )
            {
                dbUser = user;
            };
        }
    }
}
