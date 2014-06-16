using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Reflection;
namespace MVVM_Uebung.Converters
{
    class Visibilityconverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isNull;
            if (value is ValueType)
            {
                var def = Activator.CreateInstance(value.GetType());
                isNull = value.Equals(def);
            }
            else isNull = value == null;
            return isNull ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
