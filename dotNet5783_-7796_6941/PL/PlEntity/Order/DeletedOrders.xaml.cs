using BlApi;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.PlEntity.Order
{
    /// <summary>
    /// Interaction logic for DeletedOrders.xaml
    /// </summary>
    public partial class DeletedOrders : Page
    {
        IBl bl;
        public DeletedOrders(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            DataContext = bl.BoOrder.GetAllDeletedOrders().Select(x=>x.CopyBoOrderToPoOrder());
        }
    }
}
