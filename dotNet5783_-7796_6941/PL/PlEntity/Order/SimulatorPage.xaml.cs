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

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for SimulatorPage.xaml
/// </summary>
public partial class SimulatorPage : Page
{
    //public event PropertyChangedEventHandler PropertyChanged;

    IBl bl;
    BackgroundWorker worker;
    List<PO.OrderForList> orders;

    private DateTime Today;


  //  private ObservableCollection<PO.OrderForList> _OrdersForShow;
    public ObservableCollection<PO.OrderForList> OrdersForShow;
    //{
    //    get
    //    { return _OrdersForShow; }
    //    set
    //    {
    //        _OrdersForShow = value;
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs("OrdersForShow"));
    //        }
    //    }
    //}

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

        OrdersForShow = new(PO.Tools.GetAllOrdersInPO());
        DataContext = OrdersForShow;

        Today = DateTime.Now;
        Date.Content = Today.ToShortDateString();
    }


    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            orders = bl.BoOrder.GetAllOrderForList().Select(x => x.CopyPropTo(new PO.OrderForList())).ToList();
            bool AllIsDone = false;

            while (!AllIsDone || worker.CancellationPending == true)
            {
                PO.Order? myO;

                BO.OrderForList? myOFL;


                foreach (PO.OrderForList? Item in orders)
                {
                    myO = bl.BoOrder.GetOrdertDetails(Item.OrderID).CopyBoOrderToPoOrder();

                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    switch (myO.Status)
                    {
                        case PO.OrderStatus.Accepted:
                            if (Today.Subtract(myO.DateOrder) > new TimeSpan(5, 0, 0, 0))
                                bl.BoOrder.UpdateOrderShipping(Item.OrderID, Today);
                            break;

                        case PO.OrderStatus.Processing:

                            if (Today.Subtract(myO.ShippingDate ?? throw new Exception("בעיה,בדקו למה נוצרתי:)")) > new TimeSpan(10, 0, 0, 0))
                                bl.BoOrder.UpdateOrderDelivery(Item.OrderID, Today);
                            break;
                    }

                    Thread.Sleep(500);
                    worker.ReportProgress(Item.OrderID);
                }



                Thread.Sleep(50);
                Today = Today.AddDays(1);
                //Date.Content = Today.ToShortDateString();
                orders = bl.BoOrder.GetAllOrderForList().Select(x => x.CopyPropTo(new PO.OrderForList())).ToList();
                //AllIsDone = orders.TrueForAll(x => x?.Status == PO.OrderStatus.Completed);
                OrdersForShow = new(orders);
                //DataContext = OrdersForShow;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
               MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
    }

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        int id = e.ProgressPercentage;

        var myOFL = OrdersForShow.FirstOrDefault(x => x.OrderID == id);
        if (myOFL != null)
        {
            myOFL.Status = (PO.OrderStatus)bl.BoOrder.GetOrderForList(id).Status;
        }

        Date.Content = Today.ToShortDateString();

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

    private void StartSimulator_Click(object sender, RoutedEventArgs e)
    {
        if (worker.IsBusy == false)
        {
            startSimulator.IsEnabled = false;
            stopSimulator.IsEnabled = true;
            worker.RunWorkerAsync();
            Today = DateTime.Now;
            Date.Content = Today.ToShortDateString();
        }
    }

    private void StopSimulator_Click(object sender, RoutedEventArgs e)
    {
        //if (worker.IsBusy == true)
        {
            startSimulator.IsEnabled = true;
            stopSimulator.IsEnabled = false;
            worker.CancelAsync();

        }
    }

    private void orderTraking_Click(object sender, EventArgs e)
    {
        PO.OrderForList or = (PO.OrderForList)((DataGrid)sender).SelectedItem;
        new orderTrakingForC_Window(bl, or.OrderID).Show();
    }
}