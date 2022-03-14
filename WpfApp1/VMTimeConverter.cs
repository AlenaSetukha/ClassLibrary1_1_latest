using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using ClassLibrary1;

namespace WpfApp1
{
    public class VMTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    VMTime val = (VMTime)value;
                    string res = "Time_rel_HA: " + val.time_rel_HA.ToString("F2") +
                        " Time_rel_EP: " + val.time_rel_EP.ToString("F2");
                    return res;
                }
                return "";
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка VMTimeConverter.Convert");
                return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new VMTime();
        }
    }
}
