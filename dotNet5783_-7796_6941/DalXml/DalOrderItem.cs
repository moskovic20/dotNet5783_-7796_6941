﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using static Dal.DalXml;

namespace Dal;

///////////////////////////////////////////
//implement ILecturer with XML Serializer
//////////////////////////////////////////

internal class DalOrderItem : IOrderItem
{
    const string s_OrderItem = "OrderItem";
  //  DalXml _DXml = DalXml.Instance!;
    public int Add(Do.OrderItem item)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem?>(s_OrderItem);

        if (listOrderItems.Exists(ord => ord?.ID == item.ID && ord?.IsDeleted != true)) 
            throw new Do.AlreadyExistException("OrderItem already exist");

        Do.OrderItem? temp = listOrderItems.FirstOrDefault(ord => ord?.ID == item.ID && ord?.IsDeleted == true);

        if (temp == null)//לא היה קיים
        {

            List<configNumbers?> runningList = XMLTools.LoadListFromXMLSerializer<configNumbers?>("config");//המספרים הרצים לפני הוספת ההזמנה החדשה

            configNumbers runningNum = (configNumbers)(from Number in runningList
                                                       where Number != null
                                                       where (string)Number.GetValueOrDefault().typeOfnumber == "Num For OrderItem ID"
                                                       select Number).FirstOrDefault()!;
            ;

            runningList.Remove(runningNum);
            runningNum.numberSaved++;
            item.ID = (int)runningNum.numberSaved;//המספר הזמנה הרץ הבא

            runningList.Add(runningNum);//שמירה בהתאם בקובץ קונפיג
            XMLTools.SaveListToXMLSerializer(runningList, "config");//הרשימה לאחר ההוספה
        }
        else
            item.ID = temp.GetValueOrDefault().ID;//שימוש במספר המשוייך בלי לשנות את הקונפיג

        listOrderItems.Add(item);//להוסיף שינוי בקונפיג?
        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItem);

        return item.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem?>(s_OrderItem);
        Do.OrderItem orderI= GetById(id);
        
        if (listOrderItems.RemoveAll(p => p?.ID == id && p?.IsDeleted != true) == 0)
            throw new Do.DoesntExistException("OrderItem is missing");

        orderI.IsDeleted = true;
        listOrderItems.Add(orderI);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItem);//טעינה לקובץ עם המחוק לארכיון
    }

    public IEnumerable<Do.OrderItem?> GetAll(Func<Do.OrderItem?, bool>? filter = null) //להוסיף את הקטע עם המחוקים בפילטר
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<Do.OrderItem?>(s_OrderItem)!;
        return filter == null ? listOrderItems.Where(OI =>OI.GetValueOrDefault().IsDeleted != true).OrderBy(OI => ((Do.OrderItem)OI!).ID)
                               :listOrderItems.Where(OI => OI.GetValueOrDefault().IsDeleted != true).Where(x=>filter(x)).OrderBy(OI => ((Do.OrderItem)OI!).ID);
    }

    public IEnumerable<Do.OrderItem?> GetAlldeleted(Func<Do.OrderItem?, bool>? filter = null)
    {

        var listLOrders = XMLTools.LoadListFromXMLSerializer<Do.OrderItem?>(s_OrderItem)!;
        return filter == null ?  listLOrders.Where(O => O.GetValueOrDefault().IsDeleted == true).OrderBy(O => ((Do.OrderItem)O!).ID)
            : listLOrders.Where(O => O.GetValueOrDefault().IsDeleted == true).Where(x => filter(x)).OrderBy(O => ((Do.OrderItem)O!).ID);

    }


    public Do.OrderItem GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<Do.OrderItem?>(s_OrderItem).FirstOrDefault(p => p?.ID == id && p?.IsDeleted != true)
         ?? throw new Do.DoesntExistException("OrderItem is missing");

    /// <summary>
    ///  הפונקציה מקבלת מזהה של מוצר ושל הזמנה ומחזירה את הפריט בהזמנה שמתאים לשני המזהים
    /// </summary>
    /// <param name="OrderID"></param>
    /// <param name="ProductID"></param>
    /// <returns></returns>
    /// <exception cref="Do.DoesntExistException"></exception>
    public Do.OrderItem GetByOrderIDProductID(int OrderID, int ProductID) => //GetAll with filter
        GetAll(p => p?.OrderID == OrderID && p?.ProductID == ProductID && p?.IsDeleted != true).FirstOrDefault()
         ?? throw new Do.DoesntExistException("OrderItem is missing");

    public IEnumerable<Do.OrderItem?> GetListByOrderID(int OrderID)=>
        GetAll(p => p?.OrderID == OrderID) ?? throw new Do.DoesntExistException("OrderItem is missing");


    public void Update(Do.OrderItem item)
    {
        Delete(item.ID);
        Add(item);
    }

}