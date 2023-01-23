using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Data;

namespace PL.IvalueConvertor;

internal class StatusToProgressBar: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Random R = new Random();

        if (!(value is PO.OrderStatus))
            throw new Exception("בעיה,בואו לבדוק למה נוצרתי :)");
        var myStatus = (PO.OrderStatus)value;
        switch(myStatus)
        {
            case PO.OrderStatus.Accepted:
                return R.Next(34);
                //break;
            case PO.OrderStatus.Processing:
                return R.Next(34,67);
                //break;
           // case PO.OrderStatus.Completed:
                //return R.Next(67,101);
               // break;
        }

        return 100;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("אין מימוש");
    }
}
