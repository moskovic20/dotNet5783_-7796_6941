using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Xml.Linq; //for XElement
using Do;
using System.Xml.XPath;
using System.ComponentModel;
using System.Reflection;
using DO;

namespace Dal;


///////////////////////////////////////
//implement IStudent with linq to XML
///////////////////////////////////////

internal class Product : IProduct
{
    //DalXml _DXml = DalXml.Instance!;
    const string s_Product = "Product";


#region xmlConvertor יהודה

internal static XElement itemToXelement<Item>(Item item, string name)
    {
        IEnumerable<PropertyInfo> items = item!.GetType().GetProperties();

        IEnumerable<XElement> xElements = from propInfo in items
                                          select new XElement(propInfo.Name, propInfo.GetValue(item)!.ToString());

        return new XElement(name, xElements);
    }

    internal static Item xelementToItem<Item>(XElement xElement) where Item : new()
    {
        Item newItem = new Item();

        IEnumerable<XElement> elements = xElement.Elements();

        Dictionary<string, PropertyInfo> items = newItem.GetType().GetProperties().ToDictionary(k => k.Name, v => v);

        foreach (var item in elements)
        {
            if (items.ContainsKey(item.Name.LocalName))
            {
                items[item.Name.LocalName].SetValue(Convert.ChangeType(item.Value,
               items[item.Name.LocalName].PropertyType), item.Value);
            }
        }

        return newItem;
    }

    internal static IEnumerable<Item> xelementToItems<Item>(XElement xElement) where Item : new()
    {
        return from element in xElement.Elements()
               select xelementToItem<Item>(element);
    }

    static IEnumerable<string> GetEnumDescriptions<TEnum>() where TEnum : struct, Enum
    {
        var enumType = typeof(TEnum);

        IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();

        IEnumerable<string> descriptions = from enumValue in enumValues
                                           let fieldInfo = enumType.GetField(enumValue.ToString())
                                           let attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute
                                           select attribute?.Description ?? enumValue.ToString();

        return descriptions;
    }
    #endregion

    private Do.Product? GetProduct(XElement s) =>
     s.ToIntNullable("ID") is null ? null : new Do.Product()
     {
         ID = (int)s.Element("ID")!,
         NameOfBook = (string?)s.Element("NameOfBook"),
         AuthorName = (string?)s.Element("AuthorName"),
         Category = (CATEGORY)((string?)s.Element("Category")).ConvertEnum(),//לבדוק שעובד
         Summary = (string?)s.Element("Summary"),
         Price = (double?)s.ToDoubleNullable("Price")!,
         InStock = (int?)s.Element("InStock"),
         IsDeleted= (bool)s.Element("IsDeleted")!, //לבדוק שעובד
         ProductImagePath = (string?)s.Element("ProductImagePath"), //s.ToXPathNavigable
     };

    static IEnumerable<XElement> createProductElement(Do.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.NameOfBook is not null)
            yield return new XElement("NameOfBook", product.NameOfBook);
        if (product.AuthorName is not null)
            yield return new XElement("AuthorName", product.AuthorName);
        //if (product.Category is not null) //אצלינו הוא לא יכול להיות נל בשביל הקופיפרופטו
            yield return new XElement("StudentStatus", product.Category);
        if (product.Summary is not null)
            yield return new XElement("Summary", product.Summary);
        if (product.Price is not null)
            yield return new XElement("Price", product.Price);
        if (product.InStock is not null)
            yield return new XElement("InStock", product.InStock);
       // if (product.IsDeleted is not null) 
           yield return new XElement("IsDeleted", product.IsDeleted);
        if (product.ProductImagePath is not null)
            yield return new XElement("ProductImagePath", product.ProductImagePath);
    }

    public int Add(Do.Product item) // שלנו config לזכור לעדכן את
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        if (XMLTools.LoadListFromXMLElement(s_Product)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == item.ID && st.ToBool("IsDeleted") != true) is not null)
            throw new Do.AlreadyExistException("id already exist");

        ////Do.Product? pTemp = GetProduct(XMLTools.LoadListFromXMLElement(_DXml.ProductPath)?.Elements()
        ////    .FirstOrDefault(st => st.ToIntNullable("ID") == item.ID && st.ToBool("IsDeleted") == true));

        //if(pTemp != null)
        //{
        //    //קיים ומחוק אז רק לשנות ערך בלי להסתבך
        //}

        productsRootElem.Add(new XElement("Product", createProductElement(item)/*productsRootElem(item)*/));// מוריה תצילי אותי איך עושים פה מספר זהות לא רץ?
        XMLTools.SaveListToXMLElement(productsRootElem, s_Product);

        return item.ID; ;
    }

    public void Delete(int id)//isdeleted | can it gat change by using()?
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        Do.Product pToUp = GetById(id);

        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == id && st.ToBool("IsDeleted") != true) ??
                throw new Do.DoesntExistException("missing id"))
            .Remove();

        pToUp.IsDeleted= true;
        productsRootElem.Add(new XElement("Product", createProductElement(pToUp)));
        XMLTools.SaveListToXMLElement(productsRootElem, s_Product);//שמירה על הקובץ המעודכן עם המחוק
    }


    public IEnumerable<Do.Product?> GetAll(Func<Do.Product?, bool>? filter = null) =>//לשים לב לפילטר עם מחוקים
        filter is null 
        ? XMLTools.LoadListFromXMLElement(s_Product).Elements().Select(s => GetProduct(s))
        : XMLTools.LoadListFromXMLElement(s_Product).Elements().Select(s => GetProduct(s)).Where(filter);


    public Do.Product GetById(int id) =>
        (Do.Product)GetProduct(XMLTools.LoadListFromXMLElement(s_Product)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        ?? throw new Do.DoesntExistException("missing id"))!;


    public Do.Product GetByName(string name) =>
        (Do.Product)GetProduct(XMLTools.LoadListFromXMLElement(s_Product)?.Elements()
        .FirstOrDefault(st => (string?)st.Element("NameOfBook") == name)
        ?? throw new Do.DoesntExistException("missing name"))!;

    public void Update(Do.Product item)//try catch?
    {
        Delete(item.ID);
        Add(item);
    }

    public IEnumerable<Do.Product?> GetAlldeleted()
    {
        var list=XMLTools.LoadListFromXMLElement(s_Product).Elements().Select(s => GetProduct(s));
        return from p in list
               where p?.IsDeleted == true
               select p;

    }
}
