using PL.PO;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.IvalueConvertor;

class DateTimeToString : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return "";

        DateTime myDT = (DateTime)value;
        return myDT.ToShortDateString();
       
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("אין מימוש");
    }
}