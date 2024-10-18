using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //Constructor
        public User(string username, string password, string Country, string SecurityQuestion, string SecurityAnswer) : base(username, password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
        }

        public User(string username, string password) : base(username, password)
        {

        }

        public static List<User> Users = new List<User>();
        

        //Methods
        public override void SignIn(string Username, string Password)   //Tar username och passsword som argument från inloggningsfönstret
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(string SecurityAnswer)
        {

        }

         
    }
}
