using DataBaseContext.EF;
using DataBaseContext.Interfaces;
using DataBaseContext.Models.Good;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseContext.Repositories
{
    public class GoodRepository : IRepository<Good>
    {
        private Context DB;

        public GoodRepository(Context context)
        {
            DB = context;
        }

        public void Create(Good item)
        {
            DB.Goods.Add(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            Good concreteGood = DB.Goods.ToList().
                Find(item => item.Id == id);

            if (concreteGood != null)
            {
                DB.Goods.Remove(concreteGood);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Contact has not found");
        }

        public Good Read(int id)
        {
            Good good = DB.Goods.Find(id);
            good.Apartment = DB.Apartments.ToList().Find(x => x.GoodId == good.Id);
            good.Rieltor = DB.Clients.ToList().Find(x => x.Id == good.Rieltor.Id);

            return good;
        }

        public IEnumerable<Good> ReadAll()
        {
            IEnumerable<Good> goods = DB.Goods.ToList();

            foreach (Good good in goods)
            {
                good.Apartment = DB.Apartments.ToList().Find(x => x.GoodId == good.Id);
                good.Rieltor = DB.Clients.ToList().Find(x => x.Id == good.Rieltor.Id);
            }

            return goods;
        }

        public void Update(Good item)
        {
            var concreteGood = Read(item.Id);

            if (concreteGood != null)
            {
                concreteGood.Describe = item.Describe;
                concreteGood.Status = item.Status;
                concreteGood.DateOfUpdate = item.DateOfUpdate;
                concreteGood.DateOfAdd = item.DateOfAdd;
                concreteGood.Link = item.Link;
                concreteGood.Price = item.Price;
                concreteGood.Rieltor = item.Rieltor;
                concreteGood.Status = item.Status;

                DB.Goods.Update(concreteGood);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Good has not found");
        }
    }
}
