using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FinalTaskClient.Models.DatabaseModels;

namespace FinalTaskClient.Models
{
    class CountryCodeToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is string code)) return "-";
            if (!(values[1] is int langId)) return "-";
            Country country = ClientWorker.Countries.FirstOrDefault(c => c.Code == code);
            if (country == null) return "-";
            if (langId == 3) return country.Txt4;
            if (langId == 2) return country.Txt3;
            if (langId == 1) return country.Txt2;
            return country.Txt1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (!(value is string countryName)) return null;
            Country country = ClientWorker.Countries.FirstOrDefault(c => c.Txt1 == countryName);
            if (country != null)
            {
                return new object[]{country.Code, 0};
            }
            country = ClientWorker.Countries.FirstOrDefault(c => c.Txt2 == countryName);
            if (country != null)
            {
                return new object[] { country.Code, 1 };
            }
            country = ClientWorker.Countries.FirstOrDefault(c => c.Txt3 == countryName);
            if (country != null)
            {
                return new object[] { country.Code, 2 };
            }
            country = ClientWorker.Countries.FirstOrDefault(c => c.Txt4 == countryName);
            if (country != null)
            {
                return new object[] { country.Code, 3 };
            }

            return null;
        }
    }
}
