using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class CardioWorkout : Workout
    {

        //Propeties
        public int Distance { get; set; }

        //Contructor
        public CardioWorkout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration, int CaloriesBurned, string Notes, int Distance)
            : base(Date, TypeOfWorkOut, Duration, CaloriesBurned, Notes)
        {
            this.Distance = Distance;

        }
        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
