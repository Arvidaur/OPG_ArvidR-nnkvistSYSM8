using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder
{
    public abstract class Workout
    {
        //Properties
        public DateTime Date { get; set; }
        public string TypeOfWorkOut { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        //Method
        public abstract int CalculateCaloriesBurned();

    }
}
