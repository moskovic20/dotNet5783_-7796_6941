using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using BO;

namespace BlImplementation;

internal class BoProduct: IProduct
{
    private IDal dal = new Dal.DalList();
}
