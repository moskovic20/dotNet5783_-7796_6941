using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO;

public class MainPo : INotifyPropertyChanged //מקווה שפסדר שיהיה פבליק
{
    public event PropertyChangedEventHandler? PropertyChanged; //יטפל לנו בשינויים למסך ומהמסך


    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
    }
}
