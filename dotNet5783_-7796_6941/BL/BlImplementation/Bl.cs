﻿using BlApi;
using BO;

namespace BlImplementation;

sealed internal class Bl : IBl
{

    private static IBl? instance;
    private static readonly object key = new(); //Thread Safe

    public static IBl Instance()
    {
        if (instance == null) //Lazy Initialization
        {
            lock (key)
            {
                if (instance == null)
                    instance = new Bl();
            }
        }

        return instance;
    }

    private Bl() { }

    public IOrder BoOrder { set; get; } = new BoOrder();

    public IProduct BoProduct { set; get; } = new BoProduct();

    public ICart Cart { set; get; } = new BoCart();
}
