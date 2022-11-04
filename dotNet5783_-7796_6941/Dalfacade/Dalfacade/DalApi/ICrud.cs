﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : struct //נסביר בהמשך את כל הכותרת 
    {
        int Add(T item);
        T GetById(int id);
        void Update(T item);
        void Delete(int id);

        //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
        IEnumerable<T> GetAll();
    }

}
