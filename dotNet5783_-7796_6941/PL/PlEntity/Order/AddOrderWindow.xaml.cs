using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for AddOrderWindow.xaml
/// </summary>
public partial class AddOrderWindow : Window
{
    private IBl bl;
    Action<int> action;
    public AddOrderWindow(IBl bl, Action <int> action)
    {
        InitializeComponent();
        this.bl = bl;
        this.action = action;
    }
}
