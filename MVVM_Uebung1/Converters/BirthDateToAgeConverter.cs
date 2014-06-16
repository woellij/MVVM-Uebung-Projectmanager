using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MVVM_Uebung.Converters
{
    class BirthDateToAgeConverter : Windows.UI.Xaml.Data.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTimeOffset)value;
            var zero = DateTime.MinValue;

            var ageDate = zero.Add(DateTime.Now - date);

            switch ((string)parameter)
            {
                case "Days":
                    return (DateTimeOffset.Now - date).TotalDays;
                case "Years":
                    return ageDate.Year - 1;
                case "Hours":
                    return (DateTime.Now - date).TotalHours;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
