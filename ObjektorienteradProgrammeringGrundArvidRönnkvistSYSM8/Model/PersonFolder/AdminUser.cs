using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder
{
    public class AdminUser : User
    {
        //Method

        //Constructor
        public AdminUser(string username, string password) : base (username, password)
        {
            username = "admin";
            password = "admin";
        }

        public void ManageAllWorkouts()
        {

        }
    }
}
