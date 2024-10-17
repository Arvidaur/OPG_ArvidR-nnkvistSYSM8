using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    class StrengthWorkout : Workout
    {
        public int Repetitions { get; set; }
        public override int CalculateCaloriesBurned()
        {
            throw new NotImplementedException();
        }
    }
}
