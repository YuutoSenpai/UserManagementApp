using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApp
{
    public class Admin : User
    {
        public Admin() { }
        public Admin(string username, string password) : base(username, password) { }
    }
}
