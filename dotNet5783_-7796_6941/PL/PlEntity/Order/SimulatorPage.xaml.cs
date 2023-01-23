using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.PO;
using System.Media;
//using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for SimulatorPage.xaml
/// </summary>
public partial class SimulatorPage : Page
{
    IBl bl;
    BackgroundWorker worker;
    bool isWork = false;
    private ObservableCollection<PO.OrderForList> allOrders = new();


    public  System.Media.SystemSound Beep { get; }

    public SimulatorPage(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        InitializeComponent();
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        allOrders = new(PO.Tools.GetAllOrdersInPO());
        DataContext = allOrders;

    }



    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        var Worker = sender as BackgroundWorker;
        string file, path;

        OrderForList? myOFL;

        var orders = bl.BoOrder.GetAllOrderForList().Select(x => bl.BoOrder.GetOrdertDetails((int)x?.OrderID!)).OrderBy(x => x.DateOrder).Select(x => x.CopyBoOrderToPoOrder());
        foreach (PO.Order? Item in orders)
        {
            switch (Item.Status)
            {
                case OrderStatus.Accepted:
                    bl.BoOrder.UpdateOrderShipping(Item.OrderID);
                    break;

                case OrderStatus.Processing:
                    bl.BoOrder.UpdateOrderDelivery(Item.OrderID);
                    path = @"..\PL\Sounds\menu_done.wav";
                    file = System.IO.Path.Combine(Environment.CurrentDirectory, path);
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
                    player.Play();
                    break;
            }

            myOFL = allOrders.FirstOrDefault(x => x.OrderID == Item.OrderID);
            if (myOFL != null)
            {
                myOFL.Status = (PO.OrderStatus)bl.BoOrder.GetOrderForList(Item.OrderID).Status;
            }

            System.Threading.Thread.Sleep(500);
        }


        //object obj = e.Argument;

        //worker.ReportProgress(1);

        //e.Result = "result";

        //while (worker.CancellationPending == false)
        //  {
        // Perform a time consuming operation and report progress.
        //   System.Threading.Thread.Sleep(500);
        //  worker.ReportProgress(1);
        // }
        //   e.Cancel = true;

    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        OrderForList? myOFL;

        var orders = bl.BoOrder.GetAllOrderForList().Select(x => bl.BoOrder.GetOrdertDetails((int)x?.OrderID!)).OrderBy(x => x.DateOrder).Select(x => x.CopyBoOrderToPoOrder());
        foreach (PO.Order? Item in orders)
        {
            switch (Item.Status)
            {
                case OrderStatus.Accepted:
                    bl.BoOrder.UpdateOrderShipping(Item.OrderID);
                    break;

                case OrderStatus.Processing:
                    bl.BoOrder.UpdateOrderDelivery(Item.OrderID);
                    break;
            }

            myOFL = allOrders.FirstOrDefault(x => x.OrderID == Item.OrderID);
            if (myOFL != null)
            {
                myOFL.Status = (PO.OrderStatus)bl.BoOrder.GetOrderForList(Item.OrderID).Status;
            }

            System.Threading.Thread.Sleep(500);
        }

        //int a = 0;
    }

    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        object result = e.Result;
    }

    private void startSimulator_Click(object sender, RoutedEventArgs e)
    {
        if (isWork == false)
        {
            isWork = true;
            startSimulator.IsEnabled = false;
            stopSimulator.IsEnabled = true;
            worker.RunWorkerAsync("argument");
        }
    }

    private void stopSimulator_Click(object sender, RoutedEventArgs e)
    {
        if (isWork == true)
        {
            isWork = false;
            startSimulator.IsEnabled = true;
            worker.CancelAsync();
        }
    }
}
