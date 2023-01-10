﻿using PL.PO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.IvalueConvertor;

internal class NullToEmptyString: IValueConverter
{
    private string? kind;

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is double?)
        {
            kind = "double";
            double? myDouble = (double?)value;

            if (myDouble == null)
                return "";
            else
                return myDouble;
        }

        kind = "int";

        int? myInt = (int?)value;

        if (myInt == null)
            return "";
        else
            return myInt;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {

        string myString = (string)value;

        if (myString == "")
            return null;
        else
            return (kind=="int")? int.Parse(myString):double.Parse(myString);

    }
}