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
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class WorkoutDetailsViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        private Workout _selectedWorkout;
        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }
        
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
 
        private string _workoutType;
        public string WorkoutType
        {
            get => _workoutType;
            set
            {
                _workoutType = value;
                OnPropertyChanged(nameof(WorkoutType));
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
  
        private int _caloriesBurned;
        public int Calories
        {
            get => _caloriesBurned;
            set
            {
                _caloriesBurned = value;
                OnPropertyChanged(nameof(Calories));
            }
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        private int _repDis;
        public int RepDis
        {
            get => _repDis;
            set
            {
                _repDis = value;
                OnPropertyChanged(nameof(RepDis));
            }
        }

        public ICommand EditWorkout { get; }
        public ICommand SaveWorkout { get; }
        public ICommand Return { get; }

        public WorkoutDetailsViewModel(Workout selectedWorkout)
        {
            SelectedWorkout = selectedWorkout;
            WorkoutType = selectedWorkout.TypeOfWorkOut;
            Date = selectedWorkout.Date;
            Duration = selectedWorkout.Duration;
            Calories = selectedWorkout.CaloriesBurned;
            Notes = selectedWorkout.Notes;
            RepDis = selectedWorkout.RepDis;

            IsEditing = false;  

            EditWorkout = new RelayCommand(WorkoutEdit);
            SaveWorkout = new RelayCommand(WorkoutSave);
            Return = new RelayCommand(ReturnToPreviusPage);          
        }

        private void ReturnToPreviusPage(object obj)
        {
            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        private void WorkoutSave(object obj)
        {
            SelectedWorkout.TypeOfWorkOut = WorkoutType;
            SelectedWorkout.Date = Date;
            SelectedWorkout.Duration = Duration;
            SelectedWorkout.CaloriesBurned = Calories;
            SelectedWorkout.Notes = Notes;
            SelectedWorkout.RepDis = RepDis;
            
            IsEditing = false;

            OnPropertyChanged(nameof(SelectedWorkout));
            MessageBox.Show("Ändringarna har sparats!", "Success!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void WorkoutEdit(object obj)
        {
            IsEditing = true;
        }        
    }
}
