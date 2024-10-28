using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model;
using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Model.WorkoutFolder;


namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout selectedWorkout)
        {
            InitializeComponent();
            var viewModel = new WorkoutDetailsViewModel(selectedWorkout)
            {
                CloseAction = this.Close
            };
            DataContext = viewModel;    
        }

        private void lstBoxWorkouts_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
