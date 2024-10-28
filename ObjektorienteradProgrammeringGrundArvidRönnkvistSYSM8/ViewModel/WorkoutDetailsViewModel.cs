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
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class WorkoutDetailsViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set { _workouts = value; }
        }

        public ICommand EditWorkout { get; }
        public ICommand SaveWorkout { get; }
        public ICommand Return { get; }

        public WorkoutDetailsViewModel()
        {
            Workouts = new ObservableCollection<Workout>();
            EditWorkout = new RelayCommand(WorkoutEdit);
            SaveWorkout = new RelayCommand(WorkoutSave);
            Return = new RelayCommand(ReturnToPreviusPage);

            LoadUserWorkouts(); //Calling method to load the workouts in the Details Window
        }

        private void ReturnToPreviusPage(object obj)
        {
            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        private void WorkoutSave(object obj)
        {
            throw new NotImplementedException();
        }

        private void WorkoutEdit(object obj)
        {
            throw new NotImplementedException();
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
    }
}
