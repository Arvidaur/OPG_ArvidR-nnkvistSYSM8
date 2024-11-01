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
        //Properties
        public Action CloseAction { get; set; }
        private bool IsCopy = false;
        private bool _isEditing;

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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
        
        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.

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

        //utlöser OnPropertyChanged vid värdeändring för att uppdatera UI.
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

        // ICommand-egenskaper för att hantera kommandon
        public ICommand EditWorkout { get; }
        public ICommand SaveWorkout { get; }
        public ICommand Return { get; }
        public ICommand CopyWorkout { get; }

        //Konstruktor
        public WorkoutDetailsViewModel(Workout selectedWorkout)
        {
            //Standardvärden ska vara det valda passet
            SelectedWorkout = selectedWorkout;  
            WorkoutType = selectedWorkout.TypeOfWorkOut;
            Date = selectedWorkout.Date;
            Duration = selectedWorkout.Duration;
            Calories = selectedWorkout.CaloriesBurned;
            Notes = selectedWorkout.Notes;
            RepDis = selectedWorkout.RepDis;

            IsEditing = false;  //Som default kan fälten inte redigeras tills användaren trycker på redigera knappen


            EditWorkout = new RelayCommand(WorkoutEdit);
            SaveWorkout = new RelayCommand(WorkoutSave);
            Return = new RelayCommand(ReturnToPreviusPage);
            CopyWorkout = new RelayCommand(CopySelectedWorkout);
            
        }

        //Metod som kopierar träningspasset så man kan använda det som mall för ett nytt
        private void CopySelectedWorkout(object obj)
        {
            IsCopy = true;  //När användaren sparar passet så sparas det som en kopia
            Workout copyOfSelectedWorkout = null;
            if (SelectedWorkout is StrengthWorkout)
            {
                copyOfSelectedWorkout = new StrengthWorkout(Date, WorkoutType, Duration, Calories, Notes, RepDis);
            }
            else if (SelectedWorkout is CardioWorkout)
            {
                copyOfSelectedWorkout = new CardioWorkout(Date, WorkoutType, Duration, Calories, Notes, RepDis);
            }
            
            User.ActiveUser.Workouts.Add(copyOfSelectedWorkout);
            MessageBox.Show("Kopia har skapats" , "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Metod för att återgå till tidigare fönster
        private void ReturnToPreviusPage(object obj)
        {
            var workoutsWindow = new WorkoutsWindow(); //Creating an instance of WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        //Metod för att spara träningspasset
        private void WorkoutSave(object obj)
        {
                SelectedWorkout.TypeOfWorkOut = WorkoutType;
                SelectedWorkout.Date = Date;
                SelectedWorkout.Duration = Duration;
                SelectedWorkout.Notes = Notes;
                SelectedWorkout.RepDis = RepDis;           

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

            if (WorkoutType == "Strength")
            {
                SelectedWorkout.CaloriesBurned = SelectedWorkout.CalculateCaloriesBurned();
            }
            else if (WorkoutType == "Cardio")
            {
                SelectedWorkout.CaloriesBurned = SelectedWorkout.CalculateCaloriesBurned();
            }



            IsEditing = false;  //När passet sparats avaktiverar vi så det inte går att redigera pass för nästa gång man vill redigera ett pass

            OnPropertyChanged(nameof(SelectedWorkout));
            MessageBox.Show("Ändringarna har sparats!", "Success!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            //Återgår till tidigare fönster
            var workoutsWindow = new WorkoutsWindow(); //Går tillbaka till WorkoutsWindow
            workoutsWindow.Show();
            CloseAction?.Invoke(); // Close the current window
        }

        //Metod som låser upp fälten för redigering 
        private void WorkoutEdit(object obj)
        {
            IsEditing = true;
            MessageBox.Show("Du kan nu redigera träningspasset!", "Editing enabled!", MessageBoxButton.OK, MessageBoxImage.Information);
        }        
    }
}
