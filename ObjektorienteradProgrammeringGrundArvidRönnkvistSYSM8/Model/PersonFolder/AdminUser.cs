﻿using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.PersonFolder
{
    public class AdminUser : User
    {

        //Properties
        public bool IsAdmin;

        //Constructor
        public AdminUser(string username, string password) : base (username, password)
        {

        }

        //Method. Används inte var med i klassdiagrammet
        public static void ManageAllWorkouts()
        {

        }        
    }
}
