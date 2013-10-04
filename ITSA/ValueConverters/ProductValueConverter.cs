using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
namespace ITSA.ValueConverters
{
    class ProductValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float a; float b;
            if (float.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out a) && float.TryParse(parameter.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out b))
                return a*b;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
