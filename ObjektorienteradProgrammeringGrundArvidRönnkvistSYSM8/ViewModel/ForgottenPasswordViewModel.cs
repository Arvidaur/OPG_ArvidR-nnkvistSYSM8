using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class ForgottenPasswordViewModel : ViewModelBase
    {

        //Properties
        Random rnd = new Random();
        private int SecurityCode;

        private string _securityQuestion;
        public string SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }
        private string _username;       

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand Code { get; }
        public ICommand Answer { get; }


        public ForgottenPasswordViewModel()
        {
            Code = new RelayCommand(GetCode);
            Answer = new RelayCommand(UpdatePassword);

            ShowSecurityQuestion();
        }

        private void UpdatePassword(object obj)
        {
            throw new NotImplementedException();
        }

        private void GetCode(object obj)
        {
            if (User.Users.Any(user => user.Username == Username))  //Om det finns en användare med användarnamnet som matas in går vi vidare
            {
                var user = User.Users.First(user => user.Username == Username); //Kan vara onödigt
                User.ActiveUser = user; //Användarnamnet som input blir den aktiva användaren vars 
            }
        }

        private void ShowSecurityQuestion()
        {
            if (User.ActiveUser != null)
            {
                SecurityQuestion = User.ActiveUser.SecurityQuestion;
            }
            else
            {
                SecurityQuestion = "No active user available."; // Default message if no user is active
            }
        }
    }
}
