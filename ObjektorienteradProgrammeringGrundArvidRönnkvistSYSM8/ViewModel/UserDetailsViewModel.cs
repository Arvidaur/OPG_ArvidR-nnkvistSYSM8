using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        private string Username, Password, Country, SecurityQuestion, SecurityAnswer;


        //private string Password;
        //private string 


        public void SaveUserDetailsTest()
        {
            User newUser = new User("arvidaur", "arvid123", "Sweden", "What is the name of your cat?", "Gocho");

            User.Users.Add(newUser);
        }

        public void SaveUserDetails()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
