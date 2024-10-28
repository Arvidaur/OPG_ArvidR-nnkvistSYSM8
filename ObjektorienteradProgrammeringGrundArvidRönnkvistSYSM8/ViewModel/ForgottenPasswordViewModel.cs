using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class ForgottenPasswordViewModel : ViewModelBase
    {
        //Properties
        public Action CloseAction { get; set; }
        Random rnd = new Random();
        private string SecurityCode;

        private string _securityQuestion, password, confirmPassword, securityAnswer, securityCodeInput;
        public string SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }
        public string SecurityCodeInput
        {
            get => securityCodeInput;
            set
            {
                securityCodeInput = value;
                OnPropertyChanged(nameof(SecurityCodeInput));
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

        public ICommand Code { get; }
        public ICommand Answer { get; }
        public ICommand Return { get; }


        public ForgottenPasswordViewModel()
        {
            Code = new RelayCommand(GetCode);
            Answer = new RelayCommand(UpdatePassword); 
            Return = new RelayCommand(ReturnToMain);
        }

        private void ReturnToMain(object obj)
        {
            var mainWindow = new MainWindow(); //Creating an instance of UserDetailWindow
            mainWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        public bool PasswordControl()
        {
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Password cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Password != ConfirmPassword)
            {
                MessageBox.Show("Password doesn't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (Password.Length < 8)
            {
                MessageBox.Show("Password has to be at least 8 characters long", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Password.Any(char.IsUpper))
            {
                MessageBox.Show("Password has to contain one upper case letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Password.Any(char.IsLower))
            {
                MessageBox.Show("Password has to contain at least one lowercase letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!Password.Any(char.IsDigit))
            {
                MessageBox.Show("Password has to contain at least one digit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else 
            {
                return true;    //Lösenordet är okej
            }
            
        }

        private void UpdatePassword(object obj)
        {
            if (PasswordControl())  //Kollar att nya lösenordet är längre än 8 teckan och innehåller en stor bokstav och siffra
            {
                if (User.ActiveUser.SecurityAnswer == SecurityAnswer && SecurityCode == SecurityCodeInput)  //Om svaret på säkerhetsfrågan stämmer och koden stämmer
                {
                    User.ActiveUser.Password = Password;
                    MessageBox.Show("Lösenordet har uppdaterats!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;

                }
                else if (SecurityCode != SecurityCodeInput)
                {
                    MessageBox.Show("Fel säkerhetskod", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (User.ActiveUser.SecurityAnswer != SecurityAnswer)
                {
                    MessageBox.Show("Fel svar på säkerhetsfrågan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else 
            {
                MessageBox.Show("Okänt fel", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void GetCode(object obj)
        {
            // Ensure ActiveUser is set
            SetActiveUser();
            if (User.ActiveUser == null) return;

            SecurityCode = ""; // Reset SecurityCode
            SecurityQuestion = User.ActiveUser.SecurityQuestion;

            for (int i = 0; i < 4; i++) //Vi genererar en slumpad kod anändaren ska skriva in för att återställa sitt lösenord
            {
                SecurityCode += rnd.Next(1, 10);
            }

            MessageBox.Show(SecurityCode, "Säkerhetskod", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void SetActiveUser()
        {
            var user = User.Users.FirstOrDefault(u => u.Username == Username);
            if (user != null)
            {
                User.ActiveUser = user;
            }
            else
            {
                MessageBox.Show("Användare med detta namn hittades inte.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
