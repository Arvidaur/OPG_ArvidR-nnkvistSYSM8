using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class CardioWorkout : Workout
    {


        //Contructor
        public CardioWorkout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration, int CaloriesBurned, string Notes, int RepDis)
            : base(Date, TypeOfWorkOut, Duration, CaloriesBurned, Notes, RepDis)
        {

        }
        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
