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

internal class Order : IOrder
{
    DalXml _DXml = DalXml.Instance!;
    public int Add(Do.Order item)
    {
        var listLOrders = XMLTools.LoadListFromXMLSerializer<Do.Order>(_DXml.OrderPath);

        if (listLOrders.Exists(ord => ord?.OrderID == item.OrderID))
            throw new Do.AlreadyExistException("Order already exist");

        listLOrders.Add(item);

        XMLTools.SaveListToXMLSerializer(listLOrders, _DXml.OrderPath);

        return item.OrderID;
    }

    public void Delete(int id)
    {
        var listLOrders = XMLTools.LoadListFromXMLSerializer<Do.Order>(_DXml.OrderPath);

        if (listLOrders.RemoveAll(p => p?.OrderID == id) == 0)
            throw new Do.DoesntExistException("Order is missing");

        XMLTools.SaveListToXMLSerializer(listLOrders, _DXml.OrderPath);
    }

    public IEnumerable<Do.Order?> GetAll(Func<Do.Order?, bool>? filter = null)
    {
        var listLOrders = XMLTools.LoadListFromXMLSerializer<Do.Order>(_DXml.OrderPath)!;
        return filter == null ?
               listLOrders.OrderBy(lec => ((Do.Order)lec!).OrderID) :
               listLOrders.Where(filter).OrderBy(lec => ((Do.Order)lec!).OrderID);
    }

    public Do.Order GetById(int id) =>
     XMLTools.LoadListFromXMLSerializer<Do.Order>(_DXml.OrderPath).FirstOrDefault(p => p?.OrderID == id)
        ?? throw new Do.DoesntExistException("Order is missing");


    public void Update(Do.Order item)
    {
        Delete(item.OrderID);
        Add(item);
    }

}