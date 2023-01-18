using PL.PO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL.IvalueConvertor;

internal class filterCategoryToFilterByPrice : IValueConverter//משתמשים בממיר זה גם עבור המרה של נראות לכפתור הסר
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BitmapSource)
            return Visibility.Visible;

        if (value == null||(string)value == "")
            return Visibility.Collapsed;
        else
            return Visibility.Visible;

  
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("אין מימוש");
    }
}
