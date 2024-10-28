using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Metrics;
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
        public Action CloseAction { get; set; }
        private static bool BaseUserCreated = false;
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

            if (!BaseUserCreated)   //Om vi redan skapat en basanvändare och admin ska vi inte göra det igen när vi öppnar main window igen
            {
                CreateBaseUser();
                BaseUserCreated = true;
            }

        }

        public void CreateBaseUser()    
        {
            User newUser = new User("Arvid", "Arvid123!", "Sverige", "Vad heter ditt husdjur?", "Gocho", RegisterViewModel.Workouts); //Skapar en basanvändare
            DateTime workoutDate1 = new DateTime(2024, 10, 28);
            TimeSpan Duration1 = new TimeSpan(0, 30, 0);
            DateTime workoutDate2 = new DateTime(2024, 10, 26);
            TimeSpan Duration2 = new TimeSpan(1, 30, 0);

            CardioWorkout cardioWorkout = new CardioWorkout(workoutDate1, "Cardio", Duration1, 500, "Sprang 5 km i slottsparken, kändes bra", 5);
            StrengthWorkout strengthWorkout = new StrengthWorkout(workoutDate2, "Strength", Duration2, 600, "Gympass, fokus på ben.", 10);

            newUser.Workouts.Add(cardioWorkout);
            newUser.Workouts.Add(strengthWorkout);

            User.Users.Add(newUser);

            AdminUser admin = new AdminUser("admin", "admin");


        }

        //Method when uses tries to log into the app, checks if a user with username and password exists and logs in if true
        private void Login(object parameter)
        {
            bool isAdmin;
            bool isValidUser;
            
            try
            {
                if (Username == "admin" && Password == "admin")
                {
                    isAdmin = true;                    
                    return;
                }
                else if (Password == null || Username == null)   // Check if user exists with the correct username and password
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
                var user = User.Users.First(user => user.Username == Username && user.Password == Password);
                User.ActiveUser = user;

                var workoutsViewModel = new WorkoutsViewModel();
                workoutsViewModel.OnUserLogin(); // Set the log in message
              
                var workoutsWindow = new WorkoutsWindow(); //Creating an instance of UserDetailWindow
                workoutsWindow.Show();
                CloseAction?.Invoke(); // Close the current window
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid username or password.");
            }
        }


        private void ForgotPassword(object parameter)
        {
            var forgottenPasswordWindow = new ForgottenPasswordWindow(); //Creating an instance of UserDetailWindow
            forgottenPasswordWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        private void Register(object parameter)
        {
            var registerWindow = new Register(); //Creating an instance of UserDetailWindow
            registerWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }        
    }
}
