using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using PL.PO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BO;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for SimulatorPage.xaml
/// </summary>
public partial class SimulatorPage : Page
{
    IBl bl;
    BackgroundWorker worker;
    //bool toStop = false;
    DateTime today;
    List<BO.OrderForList> orders;

    
    private ObservableCollection<BO.OrderForList> OrdersForShow = new();

    //public static 

    public SimulatorPage(IBl bl)
    {
        InitializeComponent();
        this.bl = bl;

        InitializeComponent();

        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerSupportsCancellation = true;
        worker.WorkerReportsProgress = true;

        OrdersForShow = new(bl.BoOrder.GetAllOrderForList());
        DataContext = OrdersForShow;

    }

    private void orderTraking_Click(object sender, EventArgs e)
    {
        BO.OrderForList or = (BO.OrderForList)((DataGrid)sender).SelectedItem;
        new orderTrakingForC_Window(bl, or.OrderID).Show();
    }

    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        for (int i=0;i < 2; i++)
        {
            orders = bl.BoOrder.GetAllOrderForList().ToList();

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                break;
            }
            else
            {
                Thread.Sleep(500);
                if (worker.WorkerReportsProgress == true)
                    worker.ReportProgress(1);
               
            }

            today = today.AddHours(1);
        }

    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
       
        PO.Order? myO;

        BO.OrderForList? myOFL;

       
        foreach (BO.OrderForList? Item in orders)
        {
            myO = bl.BoOrder.GetOrdertDetails(Item.OrderID).CopyBoOrderToPoOrder();

            switch (myO.Status)
            {
                case PO.OrderStatus.Accepted:
                    if (today.Subtract(myO.DateOrder) > new TimeSpan(3, 23, 59))
                        bl.BoOrder.UpdateOrderShipping(Item.OrderID, today);
                    break;

                case PO.OrderStatus.Processing:
                   
                    if (today.Subtract(myO.ShippingDate?? throw new Exception("בעיה,בדקו למה נוצרתי:)")) > new TimeSpan(3, 23, 59))
                        bl.BoOrder.UpdateOrderDelivery(Item.OrderID,today);
                    break;
            }

            myOFL = OrdersForShow.FirstOrDefault(x => x.OrderID == Item.OrderID);
            if (myOFL != null)
            {
                myOFL.Status = bl.BoOrder.GetOrderForList(Item.OrderID).Status;
            }

            System.Threading.Thread.Sleep(500);

            OrdersForShow = new(bl.BoOrder.GetAllOrderForList());
            DataContext = OrdersForShow;
        }

    }

    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        string file, path;

        if (e.Cancelled == true)
        {
            MessageBox.Show("הסימולטור הופסק באמצע");
        }
        else
        {
            MessageBox.Show("עשינו זאת! כל ההזמנות בוצעו בהצלחה!");
            path = @"..\PL\Sounds\menu_done.wav";
            file = System.IO.Path.Combine(Environment.CurrentDirectory, path);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
            player.Play();
        }
        this.Cursor = Cursors.Arrow;
    }

    private void startSimulator_Click(object sender, RoutedEventArgs e)
    {
        if (worker.IsBusy == false)
        {
            startSimulator.IsEnabled = false;
            stopSimulator.IsEnabled = true;
            today = DateTime.Now;
            worker.RunWorkerAsync();
            
        }
    }

    private void stopSimulator_Click(object sender, RoutedEventArgs e)
    {
        //if (worker.IsBusy == true)
        {
            startSimulator.IsEnabled = true;
            stopSimulator.IsEnabled = false;
            worker.CancelAsync();
        }
    }
}
