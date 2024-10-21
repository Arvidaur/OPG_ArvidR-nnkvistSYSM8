using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel
{
    public class AddWorkoutViewModel : ViewModelBase
    {
        public DateTime Date = DateTime.Now;
        private string workoutType;
        private TimeSpan duration;
        private int caloriesBurned;
        private string notes;
        
        private int repetitions;    //Ifall användaren vill logga ett styrkepass(Strength)
        private int distance;   //Ifall användaren vill logga ett fyspass(Cardio)
        

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
        public ICommand WorkoutAdd { get; }
        public ICommand ReturnToPrevios { get; }
    
        public AddWorkoutViewModel()
        {
            WorkoutAdd = new RelayCommand(AddWorkout);
            ReturnToPrevios = new RelayCommand(Return);
        }

        public void AddWorkout(object parameter)
        {
            if (workoutType == "Strength")
            {
               StrengthWorkout workout = new StrengthWorkout(Date, WorkoutType, Duration, CaloriesBurned, ) 
            }

        }

        public void Return(object parameter)
        {

        }
    }


}
