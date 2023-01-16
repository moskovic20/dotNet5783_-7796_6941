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

static internal class XMLTools {

    const string s_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;

    //public static DO.CATEGORY ConvertEnum(this XElement element, string caterory)//is working?
    //{ 
    //    DO.CATEGORY result;
    //    Enum.TryParse((string?)element.Element(caterory), out result);
    //    return result;
    //}

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
        bool.TryParse((string?)element.Element(name), out var result) ? (bool?) result : null;
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
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);
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
            //new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);
            throw new Do.LoadingException($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    //static readonly bool s_writing = false;
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            //using XmlWriter writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true });

            XmlSerializer serializer = new(typeof(List<T?>));
            //if (s_writing)
            //    serializer.Serialize(writer, list);
            //else
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {dir + filePath}", ex);            }
            throw new Do.LoadingException($"fail to create xml file: {filePath}", ex);
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity) 
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {dir + filePath}", ex);            }
            throw new Do.LoadingException($"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion

}

#region רעות ותמר
//{
//    static string suffixPath = @"xml\";

//    #region XElement [products to files] 

//    #region load to file
//    public static XElement LoadListFromXMLElement(string filePath)
//    {
//        try
//        {
//            if (File.Exists(suffixPath + filePath))
//            {
//                return XElement.Load(suffixPath + filePath);
//            }
//            else
//            {
//                XElement rootElem = new XElement(filePath);
//                if (filePath == @"config.xml") { }
//                //rootElem.Add(new XElement("droneRunningNum", 1)); //להוריד
//                rootElem.Save(suffixPath + filePath);
//                return rootElem;
//            }
//        }
//        catch (Exception ex)
//        {
//            throw new Do.LoadingException(filePath, $"fail to load xml file: {filePath}", ex);
//        }
//    }
//    #endregion

//    #region save to file
//    public static void SaveListToXMLElement(XElement rootElem, string filePath)
//    {
//        try
//        {
//            rootElem.Save(suffixPath + filePath);
//        }
//        catch (Exception ex)
//        {
//            throw new Do.LoadingException(suffixPath + filePath, $"fail to create xml file: {suffixPath + filePath}", ex);
//        }
//    }

//    #endregion

//    #endregion

//    #region XmlSerializer [Order & OrderItem to list] 
//    //save a complete list in a specific file- throw exception in case of problems..
//    //for the using with XMLSerializer..
//    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
//    {
//        try
//        {
//            FileStream file = new FileStream(suffixPath + filePath, FileMode.Create);
//            XmlSerializer x = new XmlSerializer(list.GetType());
//            x.Serialize(file, list);
//            file.Close();
//        }
//        catch (Exception ex)
//        {
//            throw new Do.LoadingException(suffixPath + filePath, $"fail to create xml file: {suffixPath + filePath}", ex);
//        }
//    }

//    //load a complete list from a specific file- throw exception in case of problems..
//    //for the using with XMLSerializer..
//    public static List<T>? LoadListFromXMLSerializer<T>(string filePath)
//    {
//        try
//        {
//            if (File.Exists(suffixPath + filePath))
//            {
//                List<T>? list;
//                XmlSerializer x = new XmlSerializer(typeof(List<T>));
//                FileStream file = new FileStream(suffixPath + filePath, FileMode.Open);
//                list = (List<T>)x!.Deserialize(file)!;
//                file.Close();
//                return list;
//            }
//            else
//                return new List<T>();
//        }
//        catch (Exception ex)
//        {
//            throw new Do.LoadingException(suffixPath + filePath, $"fail to load xml file: {suffixPath + filePath}", ex);
//        }
//    }
//    #endregion
//}
#endregion
