using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Xml.Linq; //for XElement
using Do;
using System.Xml.XPath;

namespace Dal;


///////////////////////////////////////
//implement IStudent with linq to XML
///////////////////////////////////////

internal class Product : IProduct
{
    DalXml _DXml = DalXml.Instance!;

    static Do.Product? GetProduct(XElement s) =>
     s.ToIntNullable("ID") is null ? null : new Do.Product()
     {
         ID = (int)s.Element("ID")!,
         NameOfBook = (string?)s.Element("NameOfBook"),
         AuthorName = (string?)s.Element("AuthorName"),
         Category = s.ToEnumNullable<DO.CATEGORY?>("Category"),//null -___-
         Summary = (string?)s.Element("Summary"),
         Price = (double)s.ToDoubleNullable("Price")!,
         InStock = (int)s.Element("InStock")!,
         IsDeleted= (bool)s.Element("IsDeleted")!, //ככה עושים עם טיפוס בוליאני??
         ProductImagePath = (string?)s.Element("ProductImagePath"), //s.ToXPathNavigable
     };

    static IEnumerable<XElement> createProductElement(Do.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.NameOfBook is not null)
            yield return new XElement("FirstName", product.NameOfBook);
        if (product.AuthorName is not null)
            yield return new XElement("LastName", product.AuthorName);
        //if (product.Category is not null) //אצלינו הוא דווקא יכול להיות נל בשביל הקופיפרופטו
        //    yield return new XElement("StudentStatus", product.Category);
        if (product.Summary is not null)
            yield return new XElement("BirthDate", product.Summary);
        if (product.Price is not null)
            yield return new XElement("Grade", product.Price);
        if (product.InStock is not null)
            yield return new XElement("Grade", product.InStock);
        //if (product.IsDeleted is not null) //גם יכול להיות נל
        //    yield return new XElement("Grade", product.IsDeleted);
        if (product.ProductImagePath is not null)
            yield return new XElement("Grade", product.ProductImagePath);
    }

    public int Add(Do.Product item) // שלנו config לזכור לעדכן את
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(_DXml.ProductPath);

        if (XMLTools.LoadListFromXMLElement(_DXml.ProductPath)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == item.ID) is not null)
            throw new Do.DoesntExistException("id already exist");

        productsRootElem.Add(new XElement("Product", productsRootElem(item)));//מוריה תצילי אותי
        XMLTools.SaveListToXMLElement(productsRootElem, _DXml.ProductPath);

        return item.ID; ;
    }

    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(_DXml.ProductPath);

        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ??
        throw new Do.DoesntExistException("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(productsRootElem, _DXml.ProductPath);
    }


    public IEnumerable<Do.Product?> GetAll(Func<Do.Product?, bool>? filter = null) =>
        filter is null 
        ? XMLTools.LoadListFromXMLElement(_DXml.ProductPath).Elements().Select(s => GetProduct(s))
        : XMLTools.LoadListFromXMLElement(_DXml.ProductPath).Elements().Select(s => GetProduct(s)).Where(filter);


    public Do.Product GetById(int id) =>
        (Do.Product)GetProduct(XMLTools.LoadListFromXMLElement(_DXml.ProductPath)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        ?? throw new Do.DoesntExistException("missing id"))!;


    public Do.Product GetByName(string name) =>
        (Do.Product)GetProduct(XMLTools.LoadListFromXMLElement(_DXml.ProductPath)?.Elements()
        .FirstOrDefault(st => (string?)st.Element("NameOfBook") == name)
        ?? throw new Do.DoesntExistException("missing name"))!;

    public void Update(Do.Product item)
    {
        Delete(item.ID);
        Add(item);
    }
}
