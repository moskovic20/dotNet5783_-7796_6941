using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

static class Copy
{
    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {
        Dictionary<string, PropertyInfo> propertyInfoTarget = target!.GetType().GetProperties()
            .ToDictionary(key => key.Name, value => value);

        IEnumerable<PropertyInfo> propertyInfoSource = source!.GetType().GetProperties();

        foreach (var item in propertyInfoSource)
        {
            if (propertyInfoTarget.ContainsKey(item.Name) && (item.PropertyType == typeof(string) || !item.PropertyType.IsClass))
            {
                Type typeSource = Nullable.GetUnderlyingType(item.PropertyType)!;
                Type typeTarget = Nullable.GetUnderlyingType(propertyInfoTarget[item.Name].PropertyType)!;

                object value = item.GetValue(source)!;

                if (value is not null)
                {
                    if (propertyInfoTarget[item.Name].PropertyType == item.PropertyType)
                        propertyInfoTarget[item.Name].SetValue(target, value);

                    else if (typeSource is not null && typeTarget is not null)
                        value = Enum.ToObject(typeTarget, value);


                }
            }
        }

        
        return target;
    }

    public static Target CopyPropToStruct<Source, Target>(this Source source, Target target) where Target : struct
    {
        object obj = target;
        source.CopyPropTo(obj);
        return (Target)obj;
    }

    public static IEnumerable<Target> CopyListTo<Source, Target>(this IEnumerable<Source> sources) where Target : new()
    => from source in sources
       select source.CopyPropTo(new Target());

    public static IEnumerable<Target> CopyListToStruct<Source, Target>(this IEnumerable<Source> sources) where Target : struct
        => from source in sources
           select source.CopyPropTo(new Target());
}

//public static Target CopyPropTo<Source, Target>(this Source source, Target target) //ציפי
//{

//    if (source is not null && target is not null) //אם שני העצמים לא ריקים
//    {
//        Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
//            .ToDictionary(p => p.Name, p => p); //יוצר מילון של צמדים עם שם של שדה והערך בו

//        IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

//        foreach (var propertyInfo in propertiesInfoSource)
//        {
//            if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
//                && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
//            {
//                propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
//            }
//        }
//    }
//    return target;
//}

//public static object CopyPropToStruct<S>(this S from, Type type)//get the typy we want to copy to //ציפי
//{
//    object? to = Activator.CreateInstance(type); // new object of the Type
//    from.CopyPropTo(to);//copy all value of properties with the same name to the new object 
//    return to!; //מחזירה את העצם אובגקט החדש
//}
