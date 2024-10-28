using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder
{
    public abstract class Person
    {
        //Properties
        public string Username { get; set; }
        public string Password { get; set; }

        //Constructor
        public Person(string username, string password)
        {
            this.Username = username;  
            this.Password = password;
        }

        //Method
        public abstract void SignIn(string Username, string Password);
    }
}
