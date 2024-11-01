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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public string SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public string SecurityCodeInput
        {
            get => securityCodeInput;
            set
            {
                securityCodeInput = value;
                OnPropertyChanged(nameof(SecurityCodeInput));
            }
        }

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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


        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        // ICommand-egenskaper för att hantera kommandon
        public ICommand Code { get; }
        public ICommand Answer { get; }
        public ICommand Return { get; }

        //Konstruktor
        public ForgottenPasswordViewModel()
        {
            Code = new RelayCommand(GetCode);
            Answer = new RelayCommand(UpdatePassword); 
            Return = new RelayCommand(ReturnToMain);
        }

        //Metod för att återgå till mainWindow
        private void ReturnToMain(object obj)
        {
            var mainWindow = new MainWindow(); //Creating an instance of UserDetailWindow
            mainWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        //MEtod där vi säkerställer att användarens lösenord uppfyller kraven på lösenord
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

        //Om lösenordet är okej uppderas anändarens lösenord
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
                return;
            }
        }

        //Metod för att ge användaren sin säkerhetskod
        private void GetCode(object obj)
        {
            
            // Ensure ActiveUser is set
            if (SetActiveUser())    //Om vi inte hittar en användare med inmatat användarnamn ska det inte gå
            {
                if (User.ActiveUser == null || User.ActiveUser.Username == "admin") return; // Om ingen användare är vald (ska inte vara något problem här) eller om admin försöker återställa sitt lösenord ska det inte fungera 

                SecurityCode = ""; // Reset SecurityCode
                SecurityQuestion = User.ActiveUser.SecurityQuestion;    //Får tag på den aktiva användarens säkerhetsfråga

                for (int i = 0; i < 6; i++) //Vi genererar en slumpad kod anändaren ska skriva in för att återställa sitt lösenord
                {
                    SecurityCode += rnd.Next(1, 10);
                }

                MessageBox.Show(SecurityCode, "Säkerhetskod", MessageBoxButton.OK, MessageBoxImage.Exclamation);    //Visar upp säkerhetskoden användaren ska skriva in för att uppdatera sitt lösenord
            }
            else
            {
                return; //Om användare inte hittas
            }
        }

        //Metod där vi settar active user baserat på användarnamn så vi får tillgång till rätt säkerhetsfråga
        public bool SetActiveUser()
        {
            var user = User.Users.FirstOrDefault(u => u.Username == Username);  //Ser om det finns en användare med det inmatade användarnamnet
            if (user != null)
            {
                User.ActiveUser = user;
                return true;    //Vi hittade en användare med användarnamnet
            }
            else
            {
                MessageBox.Show("Användare med detta namn hittades inte.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;    //Vi hittade inte en användare med användarnamnet
            }
        }
    }
}
