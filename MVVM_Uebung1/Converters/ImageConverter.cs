using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MVVM_Uebung.Converters
{
    class ImageConverter : IValueConverter
    {
        public string Default { get; set; }
        public string DefaultMale { get; set; }
        public string DefaultFemale { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var gender = (string)value;

            switch (gender)
            {
                case "männlich":
                    return CreateAppPath(DefaultMale);
                case "weiblich":
                    return CreateAppPath(DefaultFemale);
                default: return CreateAppPath(Default);
            }
        }

        private object CreateAppPath(string path)
        {
            return "ms-appx:///" + path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
