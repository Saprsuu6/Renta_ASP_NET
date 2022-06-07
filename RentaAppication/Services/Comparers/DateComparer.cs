using RentaAppication.Models.Good;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RentaAppication.Services.Comparers
{
    public class DateComparer : IComparer<Good>
    {
        public int Compare(Good x, Good y)
        {
            return x.DateOfAdd.CompareTo(y.DateOfAdd);
        }
    } 
}
