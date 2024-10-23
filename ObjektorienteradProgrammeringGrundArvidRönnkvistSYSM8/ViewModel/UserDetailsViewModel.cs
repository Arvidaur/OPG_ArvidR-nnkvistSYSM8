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
        private string username, password, confirmPassword, country, securityQuestion, securityAnswer;

        public string Username
        {
            get => username; 
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string SecurityQuestion
        {
            get => securityQuestion;
            set
            {
                securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }

        public string SecurityAnswer
        {
            get => securityAnswer;
            set
            {
                securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer));
            }
        }

        public ICommand CreateUser { get; }
        public ICommand Cancel {  get; }

        public event EventHandler CloseRequested;
        public UserDetailsViewModel()
        {
            CreateUser = new RelayCommand(createUser);
            Cancel = new RelayCommand(CancelCommand);
        }




        private void createUser(object parameter)   //Först kollar vi om all inmatning är korrekt och om den är det så skapar vi ett. !!!!!Ska implementera där jag ser till att användarnamnet inte redan existerar
        {
            bool userExists = User.Users.Any(user => user.Username == Username);
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

            MessageBox.Show($"User has been created! Username: {Username}. Password: {Password}. Country: {Country}. Security question: {SecurityQuestion}. Security answer: {SecurityAnswer}. " ,
                "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelCommand(object parameter)
        {
            
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
