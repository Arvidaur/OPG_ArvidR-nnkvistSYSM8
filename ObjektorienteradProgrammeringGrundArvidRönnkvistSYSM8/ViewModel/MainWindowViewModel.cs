using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

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
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand RegisterNewCommand { get; }

        public MainWindowViewModel()
        {
            // Initialize commands and bind them to methods
            LoginCommand = new RelayCommand(Login);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            RegisterNewCommand = new RelayCommand(Register);
        }

        //Method when uses tries to log into the app, checks if a user with username and password exists and logs in if true
        private void Login(object parameter)
        {
            bool isValidUser;
            // Check if user exists with the correct username and password
            try
            {
                if (Password == null || Username == null)
                {
                    isValidUser = false;
                    MessageBox.Show("Empty fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {                    
                    isValidUser = User.Users.Any(user => user.Username == Username && user.Password == Password);                 
                }
            }
            catch
            {
                MessageBox.Show("There are no registered users", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            if (isValidUser)
            {
                User signInUser = new User(Username, Password); //Creates an object used with the sign in method to make sure that the user logging in is the active user 
                signInUser.SignIn(Username, Password);
                var workoutsWindow = new WorkoutsWindow(); //Creating an instance of UserDetailWindow
                workoutsWindow.ShowDialog();               
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid username or password.");
            }
        }


        private void ForgotPassword(object parameter)
        {
            System.Windows.MessageBox.Show("Forgot password functionality is not implemented yet.");
        }

        private void Register(object parameter)
        {
            
            var registerWindow = new Register(); //Creating an instance of UserDetailWindow
            registerWindow.ShowDialog();
        }        
    }
}
