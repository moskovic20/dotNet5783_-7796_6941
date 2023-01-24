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
using PL.Catalog;
using System.Windows.Navigation;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for SimulatorPage.xaml
/// </summary>
public partial class SimulatorPage : Page
{
    public event PropertyChangedEventHandler PropertyChanged;

    IBl bl;
    bool AllIsDone = false;
    BackgroundWorker worker;
    List<PO.OrderForList> orders;

    private DateTime Today;


    public ObservableCollection<PO.OrderForList>? OrdersForShow
    {
        get { return (ObservableCollection<PO.OrderForList>)GetValue(OrdersForShowProperty); }
        set { SetValue(OrdersForShowProperty, value); }
    }
    public static readonly DependencyProperty OrdersForShowProperty =
        DependencyProperty.Register("OrdersForShow", typeof(ObservableCollection<PO.OrderForList>), typeof(SimulatorPage));

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
        // DataContext = OrdersForShow;

        Today = DateTime.Now;
        Date.Content = Today.ToShortDateString();
    }


    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        try
        {
            orders = bl.BoOrder.GetAllOrderForList().Select(x => x.CopyPropTo(new PO.OrderForList())).ToList();
            AllIsDone = orders.TrueForAll(x => x?.Status == PO.OrderStatus.Completed);

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
                        return;
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

                //Thread.Sleep(50);
                Today = Today.AddDays(3);
                AllIsDone = orders.TrueForAll(x => x?.Status == PO.OrderStatus.Completed);
            }
        }
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            worker.ReportProgress(-1);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.InnerException?.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK,
               MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }
        orders = bl.BoOrder.GetAllOrderForList().Select(x => x.CopyPropTo(new PO.OrderForList())).OrderBy(x => x?.OrderID).ToList();//לקיחת הרשימה המעודכת

    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

        int id = e.ProgressPercentage;

        orders = bl.BoOrder.GetAllOrderForList().Select(x => x.CopyPropTo(new PO.OrderForList())).ToList();//לקיחת הרשימה המעודכת
        OrdersForShow = new(orders);

        Date.Content = Today.ToShortDateString();

    }

    private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        string file, path;

        if (e.Cancelled == true)
        {
            MessageBox.Show("הסימולטור הופסק באמצע");
            path = @"..\PL\Sounds\menu_done.wav";
            file = System.IO.Path.Combine(Environment.CurrentDirectory, path);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
            player.Play();
        }
        else
        {
            MessageBox.Show("עשינו זאת! כל ההזמנות בוצעו בהצלחה!");
            path = @"..\PL\Sounds\sfx-victory1.wav";
            file = System.IO.Path.Combine(Environment.CurrentDirectory, path);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(file);
            player.Play();
            startSimulator.IsEnabled = true;
            stopSimulator.IsEnabled = false;
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

            //MessageBox.Show("הסימולטור הופסק באמצע");
        }
    }

    private void orderTraking_Click(object sender, EventArgs e)
    {
        PO.OrderForList or = (PO.OrderForList)((DataGrid)sender).SelectedItem;
        new orderTrakingForC_Window(bl, or.OrderID).Show();
    }
}