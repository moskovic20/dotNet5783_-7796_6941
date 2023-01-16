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
    /// Interaction logic for DeletedOrderItems.xaml
    /// </summary>
    public partial class DeletedOrderItems : Page
    {
        IBl bl;
        public DeletedOrderItems(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            DataContext = bl.BoOrder.GetAllDeletedOrderItems().Select(x => x.CopyPropTo(new PO.OrderItem()));
        }
    }
}
