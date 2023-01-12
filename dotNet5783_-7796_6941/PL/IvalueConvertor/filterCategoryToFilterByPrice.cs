using PL.PO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL.IvalueConvertor;

internal class filterCategoryToFilterByPrice : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == ""||value==null)
            return Visibility.Collapsed;
        else
            return Visibility.Visible;

  
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("אין מימוש");
    }
}
