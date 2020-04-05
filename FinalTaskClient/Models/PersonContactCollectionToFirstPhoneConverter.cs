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
    class PersonContactCollectionToFirstPhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ICollection<PersonContact> contacts)) return "-";
            return contacts.FirstOrDefault(c => c.ContactTypeId == 1)?.Txt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
