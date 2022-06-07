using RentaAppication.Models.Good;
using System.Collections.Generic;

namespace RentaAppication.Services.Comparers
{
    public class WatchngsComparer : IComparer<Good>
    {
        public int Compare(Good x, Good y)
        {
            return x.Watchings.CompareTo(y.Watchings);
        }
    }
}
