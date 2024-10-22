using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class AddWorkoutViewModel : ViewModelBase
    {
        private DateTime Date = DateTime.Now;
        private string workoutType;
        private TimeSpan duration;
        private int caloriesBurned;
        private string notes = "";
        
        private int repDis;    //Repetitions / Distance beroende på om användaren vill logga ett fys eller styrkepass
        //private int distance;  
        

        public string WorkoutType
        {
            get => workoutType;
            set
            {
                workoutType = value;
                OnPropertyChanged(nameof(WorkoutType));
            }
        }

        public TimeSpan Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int CaloriesBurned
        {
            get => caloriesBurned;
            set
            {
                caloriesBurned = value;
                OnPropertyChanged(nameof(CaloriesBurned));
            }
        }

        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public int RepDis
        {
            get => repDis;
            set
            {
                repDis = value;
                OnPropertyChanged(nameof(RepDis));
            }
        }

        

        public ICommand WorkoutAdd { get; }
        public ICommand ReturnToPrevios { get; }
    
        public AddWorkoutViewModel()
        {
            WorkoutAdd = new RelayCommand(AddWorkout);
            ReturnToPrevios = new RelayCommand(Return);
        }

        public void AddWorkout(object parameter)
        {
            if (string.IsNullOrWhiteSpace(workoutType)) //If no workout type is selected
            {
                return;
            }
            if (workoutType == "Strength")
            {
                StrengthWorkout workout = new StrengthWorkout(Date, WorkoutType, Duration, CaloriesBurned, Notes, RepDis);
                MessageBox.Show($"Workout added!", "Workout added!");
                ClearFields();
            }
            else if (workoutType == "Cardio")
            {                
                CardioWorkout workout = new CardioWorkout(Date, WorkoutType, Duration, CaloriesBurned, Notes, RepDis);
                MessageBox.Show($"Workout added!", "Workout added!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        public void Return(object parameter)
        {

        }

        private void ClearFields()
        {
            WorkoutType = string.Empty;
            Duration = TimeSpan.Zero;
            CaloriesBurned = 0;
            Notes = string.Empty;
            RepDis = 0;
        }

    }


}
