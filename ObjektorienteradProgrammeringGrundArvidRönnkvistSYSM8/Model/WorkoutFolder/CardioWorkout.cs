using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class CardioWorkout : Workout
    {
        public int Distance { get; set; }
        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
