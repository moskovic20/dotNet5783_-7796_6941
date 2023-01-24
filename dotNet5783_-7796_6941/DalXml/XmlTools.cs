using Do;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

static internal class XMLTools
{

    const string s_dir = @"..\xml\";
    static XMLTools()
    {

        #region DSאתחול ראשוני לקבצים מה
        //SaveListToXMLSerializer<Do.Product>(DataSource.Instance._Products, "Product");
        //SaveListToXMLSerializer<Do.Order>(DataSource.Instance._Orders, "Order");
        //SaveListToXMLSerializer<Do.OrderItem>(DataSource.Instance._OrderItems, "OrderItem");
        #endregion


        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    public static DO.CATEGORY ConvertEnum(this string? caterory)//iS working?
    {
        DO.CATEGORY result;
        Enum.TryParse(caterory, out result);
        return result;
    }

    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;

    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;

    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;

    public static bool? ToBool(this XElement element, string name) =>
        bool.TryParse((string?)element.Element(name), out var result) ? (bool?)result : null;
    #endregion

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new Do.LoadingException($"fail to create xml file: {filePath}", ex);
        }
    }

    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new Do.LoadingException($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new Do.LoadingException($"fail to create xml file: {filePath}", ex);
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {

            if (!File.Exists(filePath))
                return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();

        }
        catch (Exception ex)
        {
            throw new Do.LoadingException($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

}
