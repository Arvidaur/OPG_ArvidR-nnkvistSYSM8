using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class UserDetailsViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        private string CurrentUsername = User.ActiveUser.Username;  //Sparar värdet för det gamla användarnamet ifall användaren inte vill byta namn 
        
        //Fälten ska bestå av de nuvarnde värdena den aktiva användaren har
        private string username = User.ActiveUser.Username;
        private string password = User.ActiveUser.Password;
        private string confirmPassword = User.ActiveUser.Password;
        private string country = User.ActiveUser.Country;
        private string securityQuestion = User.ActiveUser.SecurityQuestion;
        private string securityAnswer = User.ActiveUser.SecurityAnswer;

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set               
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        public string Country
        {
            get => country;
            set
            {
                if (country != value)
                { 
                    country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        public string SecurityQuestion
        {
            get => securityQuestion;
            set
            {
                if (securityQuestion != value)
                {
                    securityQuestion = value;
                    OnPropertyChanged(nameof(SecurityQuestion));
                }
            }
        }

        public string SecurityAnswer
        {
            get => securityAnswer;
            set
            {
                if (securityAnswer != value)
                {
                    securityAnswer = value;
                    OnPropertyChanged(nameof(SecurityAnswer));
                }
            }
        }

        public ICommand SaveChanges { get; }
        public ICommand Cancel {  get; }

        public UserDetailsViewModel()
        {
            SaveChanges = new RelayCommand(SaveUserChanges);
            Cancel = new RelayCommand(CancelCommand);
        }




        private void SaveUserChanges(object parameter)   //Först kollar vi om all inmatning är korrekt och om den är det så skapar vi ett. !!!!!Ska implementera där jag ser till att användarnamnet inte redan existerar
        {
            bool sameUsername = CurrentUsername == Username;
            bool userExists = User.Users.Any(user => user.Username == Username && user.Username != CurrentUsername);    //Om användaren inte bytt sitt användarnamn går vi vidare. Om det finns en användare med samma användarnamn returnar vi
            if (userExists)  
            {
                MessageBox.Show("There already exists a user with this username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Username cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password doesn't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Password.Length < 8)
            {
                MessageBox.Show("Password has to be at least 8 characters long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!Password.Any(char.IsUpper))
            {
                MessageBox.Show("Password has to contain one upper case letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!Password.Any(char.IsLower))
            {
                MessageBox.Show("Password has to contain at least one lowercase letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!Password.Any(char.IsDigit))
            {
                MessageBox.Show("Password has to contain at least one digit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(Country))
            {
                MessageBox.Show("Country cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(SecurityQuestion))
            {
                MessageBox.Show("Security Question cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Security Answer cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            
            else
            {
                SaveUserDetails(parameter);
            }
        }

        private void SaveUserDetails(object parameter)
        {
            //User newUser = new User(Username, Password, Country, SecurityQuestion, SecurityAnswer);

            //User.Users.Add(newUser);
            
            //List<StrengthWorkout> strengthWorkouts = new List<StrengthWorkout>();

            MessageBox.Show($"User has been edited! Username: {Username}. Password: {Password}. Country: {Country}. Security question: {SecurityQuestion}. Security answer: {SecurityAnswer}. " ,
                "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelCommand(object parameter)
        {

            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }
    }
}
