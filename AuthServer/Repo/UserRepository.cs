using AuthServer.Exceptions;
using AuthServer.Models;
using AuthServer.Validators;
using FluentValidation;
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
        User GetUser( string userId );
        User CreateUser( string userId, User user );
        void UpdateUser( string userId, User user );
        void DeleteUser( string id );
        bool IsUserExist( string login, string password );
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserValidator _validator = new UserValidator( );
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
            if ( UserList.ContainsKey( userId ) )
                throw new DBException( "User already exist" );

            _validator.ValidateAndThrow( user );
      
            user.NickName ??= userId;

            UserList.Add( userId, user );
            return UserList.GetValueOrDefault( userId );
        }

        public void DeleteUser( string userId )
        {
            if ( UserList.ContainsKey( userId ) )
                UserList.Remove( userId );
            else
                throw new DBException( "User not exist" );
        }

        public User GetUser( string userId )
        {
            var user = UserList.GetValueOrDefault( userId );
            return user ?? throw new KeyNotFoundException( "User Not Found" );
        }

        public void UpdateUser( string userId, User user )
        {
            var dbUser = GetUser( userId );

            if ( user.UserId != null || user.Password != null  )
                CheckCredentials( dbUser, user );

            dbUser.Comment = string.IsNullOrEmpty( user.Comment ) ? dbUser.Comment : user.Comment;
            dbUser.NickName = string.IsNullOrEmpty( user.NickName ) ? dbUser.NickName : user.NickName;

            // TODO: db save 
        }

        private void CheckCredentials( User oldUser, User newUser )
        {
            var isEqual = string.Equals( oldUser.Password, newUser.Password ) && string.Equals( oldUser.UserId, newUser.UserId );

            if ( isEqual == false )
                throw new DBException( "Updation failed. Not updatable user_id and password" );

            if ( newUser.NickName == null && newUser.Comment == null )
                throw new DBException( "Updation failed. Required nickname or comment" );
        }
    }
}
