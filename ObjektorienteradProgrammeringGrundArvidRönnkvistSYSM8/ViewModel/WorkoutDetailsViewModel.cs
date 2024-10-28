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
        public Workout SelectedWorkout { get; set; }
        
        //private DateTime date = SelectedWorkout.Date;
        //private string workoutType = SelectedWorkout.TypeOfWorkOut;
        //private string confirmPassword = SelectedWorkout.;
        //private string country = SelectedWorkout.;
        //private string securityQuestion = SelectedWorkout.;
        //private string securityAnswer = SelectedWorkout.;

        public ICommand EditWorkout { get; }
        public ICommand SaveWorkout { get; }
        public ICommand Return { get; }

        public WorkoutDetailsViewModel(Workout selectedWorkout)
        {
            
            EditWorkout = new RelayCommand(WorkoutEdit);
            SaveWorkout = new RelayCommand(WorkoutSave);
            Return = new RelayCommand(ReturnToPreviusPage);

            SelectedWorkout = selectedWorkout;
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
            
        }

        
    }
}
