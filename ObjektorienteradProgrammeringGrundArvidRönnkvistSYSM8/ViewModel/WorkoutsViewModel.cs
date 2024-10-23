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
    public class WorkoutsViewModel : ViewModelBase
    {
        private ObservableCollection<Workout> _workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return _workouts; }
            set { _workouts = value; }
        }
        public ICommand AddWorkout { get; }
        public ICommand RemoveWorkout { get; }
        public ICommand DetailsWorkout { get; }
        public ICommand EditUser { get; }

        public WorkoutsViewModel()
        {
            AddWorkout = new RelayCommand(WorkoutAdd);
            RemoveWorkout = new RelayCommand(WorkoutRemove);
            DetailsWorkout = new RelayCommand(WorkoutDetails);
            EditUser = new RelayCommand(UserEdit);
        }

        public void WorkoutAdd(object parameter)
        {
            //var addWorkoutWindow = new AddWorkoutWindow(); //Creating an instance of AddWorkoutWindow
            //addWorkoutWindow.ShowDialog();
            var addWorkoutWindow = new AddWorkoutWindow();
            if (addWorkoutWindow.ShowDialog() == true) // Assuming AddWorkoutWindow returns a result
            {
                var newWorkout = addWorkoutWindow.Workout; // Get the newly created workout from AddWorkoutWindow
                Workouts.Add(newWorkout); // Add the workout to the ObservableCollection
            }
        }

        public void WorkoutRemove(object parameter)
        {

        }

        public void WorkoutDetails(object parameter)
        {

        }
        public void UserEdit(object parameter)
        {

        }
    }
}
