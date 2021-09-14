using System;
using System.Globalization;
using Xamarin.Forms;

namespace DespPlus.Converters
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringToDecimal = System.Convert.ToDecimal(value);
            var decimalToPercent = decimal.Round(stringToDecimal, 2);
            return $"{decimalToPercent}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
