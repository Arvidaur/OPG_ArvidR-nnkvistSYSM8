using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.Converters
{
    public class WorkoutTypeToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string workoutType)
            {
                return workoutType switch
                {
                    "Strength" => "Repetitions",
                    "Cardio" => "Distance",
                    _ => "Select a workout type"
                };
            }
            return "Select a workout type";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
