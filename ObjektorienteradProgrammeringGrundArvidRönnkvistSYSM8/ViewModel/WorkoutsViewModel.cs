using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class WorkoutsViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set { 
                _workouts = value;
                OnPropertyChanged(nameof(Selected)); 
                }
        }

        private Workout _selected;  //Egenskap som håller den det valda träningspasset
        public Workout Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public ICommand AddWorkout { get; }
        public ICommand RemoveWorkout { get; }
        public ICommand DetailsWorkout { get; }
        public ICommand EditUser { get; }
        public ICommand Logout { get; }
        public ICommand Info { get; }


        public WorkoutsViewModel()
        {
            Workouts = new ObservableCollection<Workout>();
            AddWorkout = new RelayCommand(WorkoutAdd);
            RemoveWorkout = new RelayCommand(WorkoutRemove);
            DetailsWorkout = new RelayCommand(WorkoutDetails);
            EditUser = new RelayCommand(UserEdit);
            Logout = new RelayCommand(LogoutUser);
            Info = new RelayCommand(ShowInfoMessage);

            OnUserLogin();
        }

        private void ShowInfoMessage(object obj)    //Visar ett infofönster
        {
            MessageBox.Show("Lägg till: Öppnar ett fönster där du kan skapa nya träningspass. \n \nTa bort: Välj ett träningpass i listan med musen och tryck på Ta bort för att ta bort träningspasset. \n \n" +
                "Detaljer: Välj ett träningspass i listan och tryck på detaljer för att få en detaljerad vy för träningspasset. \n \n" +
                "Redigera användare: Öppnar ett fönster där du kan redigera din profil. \n \nLogga ut: Tryck för att logga ut", "Information", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void LogoutUser(object obj)
        {
            //AdminUser.IsAdmin = false;    //Admin ska vara false vare sig det var en admin som var inloggad eller inte 
            User.ActiveUser = null;     //Det är ska inte finnas någon active user när användaren loggar ut
                        
            var mainWindow = new MainWindow(); //Creating an instance of UserDetailWindow
            mainWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }  

        public void WorkoutAdd(object parameter)
        {
            // Create an instance of the AddWorkoutWindow
            var addWorkoutWindow = new AddWorkoutWindow();

            // Get the ViewModel of the AddWorkoutWindow
            var addWorkoutViewModel = (AddWorkoutViewModel)addWorkoutWindow.DataContext;

            // Subscribe to the WorkoutAdded event
            addWorkoutViewModel.WorkoutAdded += (workout) => Workouts.Add(workout);

            addWorkoutWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        private void LoadUserWorkouts() // Method to load workouts for the active user, if admin will load all the workouts
        {
            string username;
            Workouts.Clear();
            if (User.ActiveUser.Username == "admin")    //Om admin loggar in ska alla träningpass synas
            {
                foreach (var user in User.Users)
                {
                    username = user.Username;

                    foreach (var workout in user.Workouts)
                    {            
                        Workouts.Add(workout);
                    }             
                }
            }
            else   //Om det inte är admin ska vi enbart visa den aktiva användaren 
            {
                if (User.ActiveUser != null)
                {
                    foreach (var workout in User.ActiveUser.Workouts)
                    {
                        Workouts.Add(workout);
                    }
                }
            }
        }

        // Call LoadUserWorkouts after login success
        public void OnUserLogin()
        {
            LoadUserWorkouts();
            WelcomeMessage = $"Välkommen {User.ActiveUser.Username}!";
        }

        public void WorkoutRemove(object parameter)
        {
            if (Selected != null)
            {
                Workouts.Remove(Selected);  //Tar bort träningspasset från UI

                User.ActiveUser.Workouts.Remove(Selected);  //Tar bort passet från workout objektet som lagrar användarens träningspass

                Selected = null;
            }
            else
            {
                MessageBox.Show("Välj ett träningspass att ta bort", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
       
        public void WorkoutDetails(object parameter)
        {
            if (Selected != null)
            {
                var workoutDetailsWindow = new WorkoutDetailsWindow(Selected);
                workoutDetailsWindow.Show();
                CloseAction?.Invoke(); // Close the current window
            }
            else
            {
                MessageBox.Show("Välj ett träningspass att se information om", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void UserEdit(object parameter)
        {
            var userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }
    }
}
