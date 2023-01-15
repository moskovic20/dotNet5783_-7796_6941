using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using static Dal.DalXml;

namespace Dal;

///////////////////////////////////////////
//implement ILecturer with XML Serializer
//////////////////////////////////////////

internal class Order : IOrder
{
    DalXml _DXml = DalXml.Instance!;
    public int Add(Do.Order item)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<Do.Order?>(_DXml.OrderPath);//הרשימה לפני ההוספה

        if (listOrders.Exists(ord => ord?.OrderID == item.OrderID && ord?.IsDeleted != true))
            throw new Do.AlreadyExistException("Order already exist");

        Do.Order? temp = listOrders.FirstOrDefault(ord => ord?.OrderID == item.OrderID && ord?.IsDeleted == true);

        if (temp == null)//לא היה קיים
        {

            List<configNumbers?> runningList = XMLTools.LoadListFromXMLSerializer<configNumbers?>(_DXml.configPath);//המספרים הרצים לפני הוספת ההזמנה החדשה
            configNumbers runningNum = (from Number in runningList
                                        where Number.typeOfnumber == "Num For Order ID"
                                        select Number).FirstOrDefault();

            runningList.Remove(runningNum);
            runningNum.numberSaved++;
            item.OrderID = (int)runningNum.numberSaved;//המספר הזמנה הרץ הבא

            runningList.Add(runningNum);//שמירה בהתאם בקובץ קונפיג
            XMLTools.SaveListToXMLSerializer(runningList, _DXml.configPath);//הרשימה לאחר ההוספה
        }
        //else
        //    item.OrderID = temp.GetValueOrDefault().OrderID;//שימוש במספר המשוייך בלי לשנות את הקונפיג
        

        listOrders.Add(item);//שמירה בהתאם בקובץ הזמנה        
        XMLTools.SaveListToXMLSerializer(listOrders, _DXml.OrderPath);//הרשימה לאחר ההוספה

        return item.OrderID;
    
    /*  לדוג איך משתמשים במספרים הרצים תמר ורעות
      #region CreateDrone
        public int CreateDrone(Drone droneToCreate)
        {
            List<Drone> dronesList = GetDroneList().ToList();
            List<ImportentNumbers> runningList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Drone Running Number")
                                           select number).FirstOrDefault();

            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            droneToCreate.ID = (int)runningNum.numberSaved;

            runningList.Add(runningNum);
            dronesList.Add(droneToCreate);

            XmlTools.SaveListToXMLSerializer(runningList, configPath);
            XmlTools.SaveListToXMLSerializer(dronesList, dronePath);

            return (int)runningNum.numberSaved;
        }
        #endregion

     */
    }

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<Do.Order?>(_DXml.OrderPath);
        Do.Order order = GetById(id);

        if (listOrders.RemoveAll(o => o?.OrderID == id && o?.IsDeleted != true) == 0)
            throw new Do.DoesntExistException("OrderItem is missing");

        order.IsDeleted = true;
        listOrders.Add(order);

        XMLTools.SaveListToXMLSerializer(listOrders, _DXml.OrderItemPath);//טעינה לקובץ עם המחוק לארכיון
    }

    public IEnumerable<Do.Order?> GetAll(Func<Do.Order?, bool>? filter = null)
    {
        var listLOrders = XMLTools.LoadListFromXMLSerializer<Do.Order?>(_DXml.OrderPath)!;
        return filter == null ? listLOrders.Where(O => O.GetValueOrDefault().IsDeleted != true).OrderBy(O => ((Do.Order)O!).OrderID)
                               :listLOrders.Where(O => O.GetValueOrDefault().IsDeleted != true).Where(filter).OrderBy(O => ((Do.Order)O!).OrderID);
    }

    public Do.Order GetById(int id) =>
     XMLTools.LoadListFromXMLSerializer<Do.Order?>(_DXml.OrderPath).FirstOrDefault(p => p?.OrderID == id && p?.IsDeleted != true)
        ?? throw new Do.DoesntExistException("Order is missing");


    public void Update(Do.Order item)
    {
        Delete(item.OrderID);
        Add(item);
    }

}