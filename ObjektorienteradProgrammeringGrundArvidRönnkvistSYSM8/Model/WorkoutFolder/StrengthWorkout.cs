using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class StrengthWorkout : Workout
    {

        //Properties
        public int Repetitions { get; set; }

        //Contructor
        public StrengthWorkout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration,  int CaloriesBurned, string Notes, int Repetitions) 
            : base(Date, TypeOfWorkOut, Duration, CaloriesBurned, Notes)
        {
            this.Repetitions = Repetitions;

        }
        //Konstructor utan notes då det är frivilligt
        public StrengthWorkout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration, int CaloriesBurned, int Repetitions)
            : base(Date, TypeOfWorkOut, Duration, CaloriesBurned)
        {
            this.Repetitions = Repetitions;

        }

        List<StrengthWorkout> strengthWorkouts = new List<StrengthWorkout>();

        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
