﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;

internal class BoOrder //: IOrder
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    //
    
    #region חריגות זמנים אופציונליות
    private static void datePosibleExceptiones(Do.Order? or)
    {
        
        if (or.GetValueOrDefault().DateOrder == null)
            throw new ArgumentNullException("cant update status, there is no info");// בדיקות אם קיים בכלל עם מה לעבוד

        if (or.GetValueOrDefault().ShippingDate == null && or.GetValueOrDefault().DeliveryDate != null)
            throw new ArgumentException("order allredy delivered but there is no info about the shipping date");


        //----------------לא נכון בשלב שכבה זו לבדוק כאלה חריגות---------------------
        //if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DateOrder > or.GetValueOrDefault().ShippingDate)
        //    throw new ArgumentException("rong information,cant be possible that DateOrder > ShippingDate");

        //if (or.GetValueOrDefault().DeliveryDate != null && or.GetValueOrDefault().DateOrder > or.GetValueOrDefault().DeliveryDate)
        //    throw new ArgumentException("rong information,cant be possiblecant be possible that DateOrder > DeliveryDate");

        //if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DeliveryDate != null
        //                                                 && or.GetValueOrDefault().ShippingDate > or.GetValueOrDefault().DeliveryDate)
        //    throw new ArgumentException("rong information,cant be possible ShippingDate > DeliveryDate");

        if (or.GetValueOrDefault().ShippingDate != null && or.GetValueOrDefault().DeliveryDate != null)//evrything allready got heandeled
            throw new ArgumentException("order allredy delivered");
    }
    #endregion

    /// <summary>
    /// הכנסת כל ההזמנות הלא ריקות לרשימה
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.OrderForList> GetAllOrderForList()
    {
        try
        {
            var orderList = from O in dal.Order.GetAllExistsBy()
                            select new BO.OrderForList()
                            {
                                OrderID = O.ID,
                                CuustomerName = O.NameCustomer,
                                Status = O.calculateStatus(),
                                AmountOfItems =O.CalculateAmountItems(),
                                TotalPrice =O.CalculatePriceOfAllItems()
                            };
            return orderList;
        }
        catch (Exception ex)
        {
            throw new BO.GetAllForListProblemException("cant give all the orders for list", ex);
        }
    }

    /// <summary>
    /// שליחת פרטי הזמנה לפי נתונים שמתאימים לשכבת הלוגיקה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    BO.Order GetOrdertDetails(int id)
    {
        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            return new BO.Order()///use insted with copyprop...
            {
                ID = myOrder.ID,
                Email = myOrder.Email,
                ShippingAddress = myOrder.ShippingAddress,
                DateOrder = myOrder.DateOrder ?? throw new ArgumentNullException("there is no vall in DateOrder"),//should be nullable?
                Status = myOrder.calculateStatus(),
                PaymentDate = myOrder.DateOrder ?? null,//should be nullable?
                ShippingDate = myOrder.ShippingDate,
                DeliveryDate = myOrder.DeliveryDate,
                Items =dal.OrderItem.GetListByOrderID(myOrder.ID).ListFromDoToBo(),//casting from list<do.ordetitem> to list<bo.orderitem> _________watch it's Tools___________
                TotalPrice = myOrder.CalculatePriceOfAllItems()
            };
        }
        catch(Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order",ex);
        }


    }

   
    /// <summary>
    /// עדכון שילוח הזמנה 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    BO.Order UpdateOrderShipping(int id)
    {
        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");
        try
        {
            Do.Order myOrder = dal.Order.GetById(id);/*?? throw new DoesntExistException("")*///בדיקות אם קיים בכלל...
            datePosibleExceptiones(myOrder);//exceptions

            if (myOrder.ShippingDate == null && myOrder.DeliveryDate == null) //____we can update like we was asked for____
            {
                //Do.Order order = myOrder;

                if (myOrder.DateOrder > DateTime.Now)
                    throw new ArgumentException("rong information,cant be possible that DateOrder > ShippingDate"); //exceptions
                else
                {
                    myOrder.ShippingDate = DateTime.Now;
                    dal.Order.Update(myOrder);
                    return GetOrdertDetails(id); ////לטפל בחריגות מהפונק הזאת   
                }
            }
            else
                throw new BO.GetDetailsProblemException("Can't get this order correct ditales");//////ok exception?
        }
        catch (Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order", ex);
        }

    }

    /// <summary>
    /// עדכון הגעת הזמנה ליעד
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    BO.Order UpdateOrderDelivery(int id)
    {

        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");
        try
        {
            Do.Order myOrder = dal.Order.GetById(id);/*?? throw new DoesntExistException("")*///בדיקות אם קיים בכלל...
            datePosibleExceptiones(myOrder);

             if (myOrder.ShippingDate != null && myOrder.DeliveryDate == null) //____we can update like we was asked for____
            {

                
                if (myOrder.ShippingDate > DateTime.Now)
                    throw new ArgumentException("rong information,cant be possible that ShippingDate > DeliveryDate");
                else
                {   
                    myOrder.DeliveryDate = DateTime.Now;
                    dal.Order.Update(myOrder);
                    return GetOrdertDetails(id); ////לטפל בחריגות מהפונק הזאת  
                }
             }
             else
                throw new BO.GetDetailsProblemException("Can't get this order correct ditales");//////ok exception?

        }
        catch (Exception ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order", ex);
        }

    }

    /// <summary>
    /// אובייקט להסבר מצב תהליך ההזמנה
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.GetDetailsProblemException"></exception>
    BO.OrderTracking GetOrderTracking(int id)
    {
        if (id < 0)
            throw new BO.GetDetailsProblemException("Negative ID");

        try
        {
            Do.Order myOrder = dal.Order.GetById(id);
            return new BO.OrderTracking()
            {
                ID = myOrder.ID,
                Status = myOrder.calculateStatus(),///------אופציה להוסיף כבונוס תאריך משוער למה שלא קיים לו ערך-------
                Tracking = myOrder.TrackingHealper()
            };
        }
        catch(Do.DoesntExistException ex)
        {
            throw new BO.GetDetailsProblemException("Can't get this order", ex);
        }
    }

    void UpdateOrder(int id)
    {

    }

}