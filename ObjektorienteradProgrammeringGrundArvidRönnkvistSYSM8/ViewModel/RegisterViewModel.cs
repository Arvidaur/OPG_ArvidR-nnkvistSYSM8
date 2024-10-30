using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    internal class RegisterViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        private string username, password, confirmPassword, country, securityQuestion, securityAnswer;
        
        public static List<Workout> Workouts = new List<Workout>();


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
        public ICommand Cancel { get; }

        public event EventHandler CloseRequested;
        public RegisterViewModel()
        {
            CreateUser = new RelayCommand(createUser);
            Cancel = new RelayCommand(CancelCommand);
        }




        private void createUser(object parameter)   //Först kollar vi om all inmatning är korrekt och om den är det så skapar vi ett object av user med inmatningen från användaren
        {
            bool userExists = User.Users.Any(user => user.Username == Username);            

            if (userExists)
            {
                MessageBox.Show("There already exists a user with this username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
            {
                MessageBox.Show("Username must be longer than 3 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            else if (!HasSpecialCharacter(Password))
            {
                MessageBox.Show("Password has to contain at least one special character (e.g., !, @, #, $, %, ^, &, *, etc.)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                SaveUserDetails(parameter); //Allt är okej och vi kan skapa en användare med inmatningen som parametrar
            }
        }

        private bool HasSpecialCharacter(string password)
        {
            // Define special characters. You can modify this set as needed.
            string specialCharacters = "!@#$%^&*()_+-=[]{}|;':\",.<>?/";
            return password.Any(c => specialCharacters.Contains(c));
        }
        private void SaveUserDetails(object parameter)
        {
           
            User newUser = new User(Username, Password, Country, SecurityQuestion, SecurityAnswer, Workouts);

            User.Users.Add(newUser);
            
            MessageBox.Show($"User has been created! Username: {Username}. Password: {Password}. Country: {Country}. Security question: {SecurityQuestion}. Security answer: {SecurityAnswer}. ",
                "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelCommand(object parameter)
        {
            var mainWindow = new MainWindow(); //Creating an instance of UserDetailWindow
            mainWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }
    }
}
