using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class StrengthWorkout : Workout
    {


        //Contructor
        public StrengthWorkout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration,  int CaloriesBurned, string Notes, int RepDis) 
            : base(Date, TypeOfWorkOut, Duration, CaloriesBurned, Notes, RepDis)
        {

        }
        
        public override int CalculateCaloriesBurned()
        {
            return ((int)(Duration.TotalMinutes * RepDis)/2);   //Strength workout burns half the amount of calories compared to cardio workout 
        }
    }
}
