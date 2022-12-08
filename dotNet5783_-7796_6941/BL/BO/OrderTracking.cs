using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO;

public class OrderTracking
{
    public int ID { set; get; }

    public OrderStatus Status { set; get; }

    public List<Tuple<DateTime, string>?>? Tracking { set; get; }

    ///------אופציה להוסיף כבונוס תאריך משוער למה שלא קיים לו ערך-------


    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
