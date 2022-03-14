using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace WpfApp1
{
    public class BankChangedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool val = (bool)value;
                if (val)
                {
                    return "Collection is changed";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка BankchangedConverter");
                return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
