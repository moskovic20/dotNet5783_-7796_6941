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
                    if (propertyInfoTarget[item.Name].PropertyType == item.PropertyType || item.PropertyType.IsEnum)
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
