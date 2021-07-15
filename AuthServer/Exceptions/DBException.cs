using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Exceptions
{
    public class DBException : Exception
    {
        public DBException( ) : base( ) { }
        public DBException( string message ) : base( message ) { }

        public DBException( string message, params object[] args )
            : base(String.Format( CultureInfo.CurrentCulture, message, args ) )
        {

        }
    }
}
