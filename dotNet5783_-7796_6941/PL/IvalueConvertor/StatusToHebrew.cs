using PL.PO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.IvalueConvertor;

internal class StatusToHebrew : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        PO.OrderStatus myStatus = (PO.OrderStatus)value;

        if (myStatus == PO.OrderStatus.Accepted)
            return PO.Hebrew_OrderStatus.נקלטה;

        if (myStatus == PO.OrderStatus.Processing)
            return PO.Hebrew_OrderStatus.נשלחה;

        return PO.Hebrew_OrderStatus.הושלמה;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        PO.Hebrew_OrderStatus hebrewStatus = (PO.Hebrew_OrderStatus)value;

        if (hebrewStatus == PO.Hebrew_OrderStatus.נקלטה)
            return PO.OrderStatus.Accepted;

        if (hebrewStatus == PO.Hebrew_OrderStatus.נשלחה)
            return PO.OrderStatus.Processing;

        return PO.OrderStatus.Completed;

    }
}
