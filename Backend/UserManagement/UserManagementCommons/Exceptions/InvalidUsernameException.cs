using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementCommons.Exceptions
{
    public class InvalidUsernameException : Exception
    {
        const string MESSAGE = "Username is already in use, please choose another one.";
        public InvalidUsernameException() : base(MESSAGE) { }
    }
}
