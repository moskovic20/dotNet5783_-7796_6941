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

        if (myStatus == PO.OrderStatus.Pending)
            return PO.Hebrew_OrderStatus.בהמתנה;

        if (myStatus == PO.OrderStatus.Processing)
            return PO.Hebrew_OrderStatus.בהכנה;

        return PO.Hebrew_OrderStatus.בוצע;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        PO.Hebrew_OrderStatus hebrewStatus = (PO.Hebrew_OrderStatus)value;

        if (hebrewStatus == PO.Hebrew_OrderStatus.בהמתנה)
            return PO.OrderStatus.Pending;

        if (hebrewStatus == PO.Hebrew_OrderStatus.בהכנה)
            return PO.OrderStatus.Processing;

        return PO.OrderStatus.Completed;

    }
}
