using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Overlay.Converters
{
    class PercentageConverter : IMultiValueConverter, IValueConverter
    {
        /// <summary>
        /// Assumes return type is a double and all values and parameter are convertable to double
        /// 1 Parameter --> value * parameter
        /// 3 parameters --> (value0 / value1) * value2
        /// 4 parameters --> ((value0 - value1) / value2) * value3
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Length == 1)
                {
                    return Convert(values[0], targetType, parameter, culture);
                }
                if (values.Length == 3)
                {
                    return (System.Convert.ToDouble(values[0]) / System.Convert.ToDouble(values[1])) * System.Convert.ToDouble(values[2]);
                }
                if (values.Length == 4)
                {
                    return ((System.Convert.ToDouble(values[0]) - System.Convert.ToDouble(values[1])) / System.Convert.ToDouble(values[2])) * System.Convert.ToDouble(values[3]);
                }
            }
            catch (Exception generalException)
            {
                return 0.0;
            }
            throw new ArgumentException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
