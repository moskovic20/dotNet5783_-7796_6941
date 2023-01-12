using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Dal;

///////////////////////////////////////////
//implement ILecturer with XML Serializer
//////////////////////////////////////////

internal class OrderItem : IOrderItem
{
    DalXml _DXml = DalXml.Instance!;
    public int Add(Do.OrderItem item)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath);

        if (listOrderItems.Exists(ord => ord?.OrderID == item.OrderID)) // IsDeleted?
            throw new Do.AlreadyExistException("OrderItem already exist");

        listOrderItems.Add(item);

        XMLTools.SaveListToXMLSerializer(listOrderItems, _DXml.OrderItemPath);

        return item.OrderID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath);

        if (listOrderItems.RemoveAll(p => p?.OrderID == id && p?.IsDeleted != true) == 0)
            throw new Do.DoesntExistException("OrderItem is missing");

        XMLTools.SaveListToXMLSerializer(listOrderItems, _DXml.OrderItemPath);
    }

    public IEnumerable<Do.OrderItem?> GetAll(Func<Do.OrderItem?, bool>? filter = null) //להוסיף את הקטע עם המחוקים בפילטר
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath)!;
        return filter == null ? listOrderItems.OrderBy(OI => ((Do.OrderItem)OI!).OrderID)
                               :listOrderItems.Where(filter).OrderBy(OI => ((Do.OrderItem)OI!).OrderID);
    }

    public Do.OrderItem GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath).FirstOrDefault(p => p?.OrderID == id && p?.IsDeleted != true)
         ?? throw new Do.DoesntExistException("OrderItem is missing");

    /// <summary>
    ///  הפונקציה מקבלת מזהה של מוצר ושל הזמנה ומחזירה את הפריט בהזמנה שמתאים לשני המזהים
    /// </summary>
    /// <param name="OrderID"></param>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    /// <exception cref="Do.DoesntExistException"></exception>
    public Do.OrderItem GetByOrderIDProductID(int OrderID, int ProductID) =>
        XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath).FirstOrDefault(p => p?.OrderID == OrderID &&
                     p.GetValueOrDefault().IsDeleted != true && p.GetValueOrDefault().ProductID == ProductID)
         ?? throw new Do.DoesntExistException("OrderItem is missing");

    public IEnumerable<Do.OrderItem?> GetListByOrderID(int OrderID)=>
        XMLTools.LoadListFromXMLSerializer<Do.OrderItem>(_DXml.OrderItemPath).FirstOrDefault(p => p?.OrderID == OrderID)
        ?? throw new Do.DoesntExistException("OrderItem is missing");


    public void Update(Do.OrderItem item)
    {
        Delete(item.OrderID);
        Add(item);
    }


}
