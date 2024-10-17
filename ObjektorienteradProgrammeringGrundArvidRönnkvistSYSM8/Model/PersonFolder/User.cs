using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder
{
    public class User : Person
    {
        //Properties

        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        //Methods
        public override void SignIn()
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(string SecurityAnswer)
        {

        }
    }
}
