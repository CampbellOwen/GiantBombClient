using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GiantBombClient.Utilities.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            TimeSpan timeSpan;
            try
            {
                timeSpan = (TimeSpan) value;
            }
            catch (InvalidCastException)
            {
                Debug.WriteLine("[DEBUG] TimeSpanToStringConverter: Casting value to TimeSpan failed");
                return string.Empty;
            }

             

            try
            {
                return timeSpan.ToString(@"hh\:mm\:ss");
            }
            catch (FormatException)
            {
                Debug.WriteLine("[DEBUG] TimeSpanToStringConverter: Formatting TimeSpan failed");
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
