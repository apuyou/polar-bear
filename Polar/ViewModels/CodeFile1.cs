// Custom class implements the IValueConverter interface.
using System.Windows.Data;
using System;
using System.Globalization;

namespace Polar
{
    public class NbPagestoPrix : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            // value is the data from the source object.
            float pages = (float) (int)value; //on unbox et ensuite on cast :p
            float prix = pages * 0.06f; //lol il connait pas l'inference de type ce truc là ?

            CultureInfo ci = new CultureInfo("fr-FR");
            ci.NumberFormat.CurrencySymbol = "€";

            return prix.ToString("C", ci);
        }

        // OSEF
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}