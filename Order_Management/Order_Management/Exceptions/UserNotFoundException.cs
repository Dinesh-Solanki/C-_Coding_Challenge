using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Exceptions
{
    internal class UserNotFoundException :Exception
    {
        public UserNotFoundException() : base("User does not exist")
        {

        }
    }
}
