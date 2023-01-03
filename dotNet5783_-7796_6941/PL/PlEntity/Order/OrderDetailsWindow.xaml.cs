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
using System.Windows.Shapes;

namespace PL.PlEntity.Order;

/// <summary>
/// Interaction logic for OrderDetailsWindow.xaml
/// </summary>
public partial class OrderDetailsWindow : Window
{
    IBl bl;
    PO.Order orderToShow;
    public OrderDetailsWindow(IBl bl, OrderForList order)
    {
        InitializeComponent();
        this.bl = bl;
        this.orderToShow = bl.BoOrder.GetOrdertDetails(order.OrderID).CopyBoOrderToPoOrder();
        this.DataContext = orderToShow;

    }
    /*
    <Grid Name="MainGrid" Background="#FFF6F6F6" Width="816" Height="446" ShowGridLines="True">
        <Rectangle
                   Height="47"
                   VerticalAlignment="Top" 
                   Grid.ColumnSpan="3" Stroke="#FF673AB7" Fill="#FF673AB7" RenderTransformOrigin="0.551,0.458" Grid.RowSpan="2"
        />

        <Label x:Name="addOrder"
               Content=":פרטי הזמנה"
               Style="{StaticResource myTitel_1}"
               Margin="58,9,0,39" HorizontalAlignment="Left" Width="112" Grid.Column="2" Grid.RowSpan="2" 
       

        
        <Label Grid.Row="1" Grid.Column="2" Content=":מספר הזמנה" HorizontalContentAlignment="Right" FontWeight="Bold" FontSize="16"/>
        <Label  Grid.Row="1" Grid.Column="1" Content="{Binding ID}" HorizontalContentAlignment="Left"  FontSize="16"/>

        <Label Grid.Row="2" Grid.Column="2" Content=":מספר הזמנה" HorizontalContentAlignment="Right" FontWeight="Bold" FontSize="16"/>
        <Label  Grid.Row="2" Grid.Column="1" Content="{Binding ID}" HorizontalContentAlignment="Left"  FontSize="16"/>


        <!--Label Content=":מספר הזמנה" HorizontalContentAlignment="Left" Height="50" Margin="649,77,-17,319" FontSize="20" FontWeight="Bold"/>
        <Label Content="{Binding ID}" HorizontalContentAlignment="Left"  Margin="463,77,169,333" FontSize="20"/>

        <Label Content=":מספר הזמנה" HorizontalContentAlignment="Left" Height="50" Margin="649,136,-17,260" FontSize="20" FontWeight="Bold"/>
        <Label Content="{Binding ID}" HorizontalContentAlignment="Left"  Margin="442,113,172,285" FontSize="20"/>
        <Label Content=":מספר הזמנה" HorizontalContentAlignment="Left" Height="50" Margin="649,175,-17,221" FontSize="20" FontWeight="Bold"/>
        <Label Content="{Binding ID}" HorizontalContentAlignment="Left"  Margin="442,136,203,285" FontSize="20"/-->



        <!--
                <Label Content="{Binding ID}" HorizontalContentAlignment="Left" />
                <Label Content=":מספר הזמנה" HorizontalContentAlignment="Left" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding CustomerName}"  HorizontalContentAlignment="Left"/>
                <Label Content=":שם הקונה" HorizontalContentAlignment="Left" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding ShippingAddress}"/>
                <Label Content=":כתובת מייל הלקוח" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding DateOrder}"/>
                <Label Content=":תאריך יצירת הזמנה" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding Status}"/>
                <Label Content=":מצב הזמנה" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding Status}"/>
                <Label Content=":מצב הזמנה" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding PaymentDate}"/>
                <Label Content=":תאריך תשלום" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding ShippingDate}"/>
                <Label Content=":תאריך שליחה" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>


            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding DeliveryDate}"/>
                <Label Content=":תאריך הגעה ליעד" HorizontalContentAlignment="Right" Height="50" Width="125" BorderBrush="AliceBlue"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal">
                <Label Content="{Binding TotalPrice}"/>
                <Label Content=":תאריך הגעה ליעד" HorizontalContentAlignment="Right" Height="50" Width="125"/>
            </StackPanel>



        </StackPanel>
        <Label Grid.Column="1" Content="Label" HorizontalAlignment="Left" Margin="335,85,0,0" VerticalAlignment="Top" Height="24" Width="34"/>

        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="409*"/>
            <ColumnDefinition Width="407*"/>
        </Grid.ColumnDefinitions-->






    </Grid> 
     */
}
