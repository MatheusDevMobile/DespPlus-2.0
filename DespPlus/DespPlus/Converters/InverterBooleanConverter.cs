﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace DespPlus.Converters
{

    public class InverterBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

}
