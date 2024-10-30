using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.MVVM;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View;
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
        public Action CloseAction { get; set; }
        private DateTime Date = DateTime.Now;
        private string workoutType;
        private TimeSpan duration;
        private int calories;
        private string notes = "";
        
        private int repDis;    //Repetitions / Distance beroende på om användaren vill logga ett fys eller styrkepass
          
        

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

        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        public int Calories
        {
            get => calories;
            set
            {
                calories = value;
                OnPropertyChanged(nameof(Calories));
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
        public ICommand Return { get; }

        //Workoutproperty to store the result
        public Workout Workout { get; private set; }
    
        public AddWorkoutViewModel()
        {
            WorkoutAdd = new RelayCommand(AddWorkout);

            Return = new RelayCommand(ReturnToPrevios);
        }
        public event Action<Workout> WorkoutAdded;
        
        public void AddWorkout(object parameter)
        {
            
            if (string.IsNullOrWhiteSpace(workoutType)) //If no workout type is selected
            {
                return;
            }
            
            if (Date > DateTime.Now)
            {
                MessageBox.Show("Datumet kan inte vara i framtiden.", "Felaktigt datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate Duration is a positive timespan
            if (Duration <= TimeSpan.Zero)
            {
                MessageBox.Show("Varaktigheten måste vara större än noll.", "Felaktig varaktighet", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RepDis <= 0)
            {
                if (WorkoutType == "Strength")
                {
                    MessageBox.Show("Repititioner kan inte vara negativt eller 0.", "Felaktigt värde för Repetitioner", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (WorkoutType == "Cardio")
                {
                    MessageBox.Show("Distans kan inte vara negativt eller 0.", "Felaktigt värde för Distans", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            Workout workout;

            if (workoutType == "Strength")
            {
                workout = new StrengthWorkout(Date, WorkoutType, Duration, 0, Notes, RepDis);
                workout.CaloriesBurned = workout.CalculateCaloriesBurned();
                Calories = workout.CaloriesBurned;
                MessageBox.Show($"Workout type: {WorkoutType}. Workout duration: {Duration}. Calories burned: {Calories} Repetitions: {RepDis}. Notes: {Notes}", "Workout added!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (workoutType == "Cardio")
            {
                
                workout = new CardioWorkout(Date, WorkoutType, Duration, 0, Notes, RepDis);
                workout.CaloriesBurned = workout.CalculateCaloriesBurned();
                Calories = workout.CaloriesBurned;
                MessageBox.Show($"Workout type: {WorkoutType}. Workout duration: {Duration}. Calories burned: {Calories}. Distance: {RepDis}. Notes: {Notes}", "Workout added!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No workouttype found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if ActiveUser exists and initialize Workouts list if necessary
            if (User.ActiveUser != null)
            {              
                User.ActiveUser.Workouts.Add(workout); // Add workout to active user's list
                WorkoutAdded?.Invoke(workout);  //Notify that the workout was created
            }
            else
            {
                MessageBox.Show("No active user found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }           
            ClearFields();
        }

        public void ReturnToPrevios(object parameter)
        {
            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        private void ClearFields()
        {
            WorkoutType = string.Empty;
            Duration = TimeSpan.Zero;            
            Notes = string.Empty;
            Calories = 0;
            RepDis = 0;
        }
    }
}
