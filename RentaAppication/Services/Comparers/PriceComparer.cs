using RentaAppication.Models.Good;
using System.Collections.Generic;

namespace RentaAppication.Services.Comparers
{
    public class PriceComparer : IComparer<Good>
    {
        public int Compare(Good x, Good y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
