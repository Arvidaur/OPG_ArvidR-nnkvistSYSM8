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
            set { _workouts = value; }
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

        public WorkoutsViewModel()
        {
            Workouts = new ObservableCollection<Workout>();
            AddWorkout = new RelayCommand(WorkoutAdd);
            RemoveWorkout = new RelayCommand(WorkoutRemove);
            DetailsWorkout = new RelayCommand(WorkoutDetails);
            EditUser = new RelayCommand(UserEdit);
            Logout = new RelayCommand(LogoutUser);
            

            LoadUserWorkouts();
        }

        private void LogoutUser(object obj)
        {
            //AdminUser.IsAdmin = false;    //Admin ska vara false vare sig det var en admin som var inloggad eller inte 
            User.ActiveUser = null;     //Det är ska inte finnas någon active user när användaren loggar ut
            MessageBox.Show("Försöker stänga");
            Application.Current.Dispatcher.Invoke(() => CloseAction?.Invoke());
        }

        public void WorkoutAdd(object parameter)
        {
          
            // Create an instance of the AddWorkoutWindow
            var addWorkoutWindow = new AddWorkoutWindow();

            // Get the ViewModel of the AddWorkoutWindow
            var addWorkoutViewModel = (AddWorkoutViewModel)addWorkoutWindow.DataContext;

            // Subscribe to the WorkoutAdded event
            addWorkoutViewModel.WorkoutAdded += (workout) => Workouts.Add(workout);

            // Open the AddWorkoutWindow as a dialog
            addWorkoutWindow.ShowDialog();
        }

        // Method to load workouts for the active user
        private void LoadUserWorkouts()
        {
            if (User.ActiveUser != null)
            {
                Workouts.Clear();
                foreach (var workout in User.ActiveUser.Workouts)
                {
                    Workouts.Add(workout);
                }
            }
        }

        // Call LoadUserWorkouts after login success
        public void OnUserLogin()
        {
            LoadUserWorkouts();
            WelcomeMessage = $"Welcome {User.ActiveUser.Username}!";
        }

        public void WorkoutRemove(object parameter)
        {
            if(Selected != null)
            {
                Workouts.Remove(Selected);  //Tar bort träningspasset från UI

                User.ActiveUser.Workouts.Remove(Selected);  //Tar bort passet från workout objektet som lagrar användarens träningspass

                Selected = null;
            }
        }
       
        public void WorkoutDetails(object parameter)
        {
            // Create an instance of the workoutDetailsWindow
            var workoutDetailsWindow = new WorkoutDetailsWindow();

            // Open the workoutDetailsWindow as a dialog
            workoutDetailsWindow.ShowDialog();
        }
        public void UserEdit(object parameter)
        {
            // Create an instance of the workoutDetailsWindow
            var userDetailsWindow = new UserDetailsWindow();

            // Open the workoutDetailsWindow as a dialog
            userDetailsWindow.ShowDialog();
        }
    }
}
