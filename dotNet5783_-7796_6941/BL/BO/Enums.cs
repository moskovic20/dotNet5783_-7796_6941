using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO;

public enum OrderStatus { Pending = 1 /*ממתין ל*/ , Processing /*מעבד*/ , Completed /*הושלם*/ }

public enum CATEGORY
{
     mystery, fantasy, history, scinence, childen, romans, cookingAndBaking, psychology, Kodesh , all
}

public enum Filters
{
    filterBYCategory,
    filterBYName,
    filterBYBiggerThanPrice,
    filterBYSmallerThanPrice,
    None
}


