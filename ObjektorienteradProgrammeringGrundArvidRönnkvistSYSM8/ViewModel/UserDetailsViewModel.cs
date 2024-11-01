﻿using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        // ICommand-egenskaper för att hantera kommandon
        public ICommand SaveChanges { get; }
        public ICommand Cancel {  get; }

        //Konstruktor
        public UserDetailsViewModel()
        {
            SaveChanges = new RelayCommand(SaveUserChanges);
            Cancel = new RelayCommand(CancelCommand);
        }




        private void SaveUserChanges(object parameter)   //Först kollar vi om all inmatning är korrekt och om den är det så skapar vi ett.
        {
            bool sameUsername = CurrentUsername == Username;
            bool userExists = User.Users.Any(user => user.Username == Username && user.Username != CurrentUsername);    //Om användaren inte bytt sitt användarnamn går vi vidare. Om det finns en användare med samma användarnamn returnar vi
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
                SaveUserDetails(parameter); //Allt är okej och vi kan redigera användaren med inmatningen som parametrar
            }
        }

        //Metod för att se om lösenordet innehåller specialtecken
        private bool HasSpecialCharacter(string password)
        {
            // Define special characters. You can modify this set as needed.
            string specialCharacters = "!@#$%^&*()_+-=[]{}|;':\",.<>?/";
            return password.Any(c => specialCharacters.Contains(c));
        }

        //Uppdaterar nya värdena
        private void SaveUserDetails(object parameter)
        {
            // Update the properties directly on the existing ActiveUser instance
            User.ActiveUser.Username = Username;
            User.ActiveUser.Password = Password; // Consider hashing passwords for security
            User.ActiveUser.Country = Country;
            User.ActiveUser.SecurityQuestion = SecurityQuestion;
            User.ActiveUser.SecurityAnswer = SecurityAnswer;
           

            MessageBox.Show($"User has been edited! Username: {Username}. Password: {Password}. Country: {Country}. Security question: {SecurityQuestion}. Security answer: {SecurityAnswer}. " ,
                "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Metod för att återgå till tidigare fönster
        private void CancelCommand(object parameter)
        {

            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }
    }
}
