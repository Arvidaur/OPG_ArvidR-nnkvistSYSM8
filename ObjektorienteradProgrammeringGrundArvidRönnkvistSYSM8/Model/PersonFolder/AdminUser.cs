using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder
{
    public class AdminUser : User
    {

        //Properties
        public bool IsAdmin;

        //Constructor
        public AdminUser(string username, string password, bool IsAdmin) : base (username, password)
        {
            username = "admin";
            password = "admin";
            this.IsAdmin = IsAdmin;
        }

        //Method
        public void ManageAllWorkouts()
        {

        }
    }
}
