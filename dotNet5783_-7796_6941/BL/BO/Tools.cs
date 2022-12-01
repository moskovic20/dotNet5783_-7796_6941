﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public static class Tools
{
    /// <summary>
    /// שיטת הרחבה עבור ToString
    /// </summary>
    /// <typeparam name="T">generic type</typeparam>
    /// <param name="t">"this" type</param>
    /// <returns></returns>
    public static string ToStringProperty<T>(this T? t)
    {

        string str = "";
        if (t == null) return str;

        foreach (PropertyInfo item in t.GetType().GetProperties())
            str += "\n" + item.Name ?? "no name" + ": " + item.GetValue(t, null);
        return str;
    }


    //copy elements of BO to DO and vice versa
    public static void CopyPropertiesTo<T, S>(this S from, T to)
    {
        foreach (PropertyInfo propTo in to.ToStringProperty().GetType().GetProperties())//loop on all the properties in the new object
        {
            PropertyInfo? propFrom = typeof(S).GetProperty(propTo.Name);//check if there is property with the same name in the source object and get it
            if (propFrom == null)
                continue;
            var value = propFrom.GetValue(from, null);//get the value of the prperty
            if (value is ValueType || value is string)
                propTo.SetValue(to, value);//insert the value to the suitable property
        }
    }

    public static object? CopyPropertiesToNew<S>(this S from, Type type)//get the typy we want to copy to 
    {
        object? to = Activator.CreateInstance(type); // new object of the Type
        from.CopyPropertiesTo(to);//copy all value of properties with the same name to the new object
        return to;
    }
}
