using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using ClassLibrary1;

namespace WpfApp1
{
    class VMAccuracyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    VMAccuracy val = (VMAccuracy)value;
                    string res = "Argument:" + val.VMAcc_max_diff[0].ToString("F2")
                            + " Value HA:" + val.VMAcc_max_diff[1].ToString("F2")
                            + " Value EP:" + val.VMAcc_max_diff[2].ToString("F2");
                    return res;
                }
                return "";
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка VMAccuracyConverter.Convert");
                return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new VMAccuracy();
        }
    }
}
