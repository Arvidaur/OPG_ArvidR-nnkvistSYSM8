using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
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
        public static User ActiveUser {  get; private set; }
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public List<Workout> Workouts { get; set; }
        public static List<User> Users { get; set; } = new List<User>();

        //Constructor
        public User(string username, string password, string Country, string SecurityQuestion, string SecurityAnswer, List<Workout> Workouts) : base(username, password)
        {
            this.Country = Country;
            this.SecurityQuestion = SecurityQuestion;
            this.SecurityAnswer = SecurityAnswer;
            this.Workouts = Workouts;
        }

        public User(string username, string password) : base(username, password)
        {

        }

        
        

        //Methods
        public override void SignIn(string Username, string Password)   //Tar username och passsword som argument från inloggningsfönstret
        {
            var user = Users.FirstOrDefault(u => u.Username == Username && u.Password == Password); //Ser vilken användare som loggas in 
            if (user != null)
            {
                ActiveUser = user;  //Tilldelar värdet ActiveUser till användaren som är inloggad
            }
        }

        public void ResetPassword(string SecurityAnswer)
        {

        }

         
    }
}
