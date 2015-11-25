using System;
using System.Windows.Data;

namespace Chemistry.Common
{
    //Класс для преобразования текста в верстке к верхнему регистру
    public class UpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var text = value as string;
            return text != null ? text.ToUpper(culture) : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
