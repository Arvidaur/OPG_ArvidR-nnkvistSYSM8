﻿using System;
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
        public int RepDis { get; set; }
        

        //Contructor
        public Workout(DateTime Date, string TypeOfWorkOut, TimeSpan Duration, int CaloriesBurned, string Notes, int RepDis)
        {
            this.Date = Date;
            this.TypeOfWorkOut = TypeOfWorkOut;
            this.Duration = Duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = Notes;
            this.RepDis = RepDis;
        }
        

        //Metod för att kalkylera hur många kalorier som bränns
        public abstract int CalculateCaloriesBurned();

    }
}
