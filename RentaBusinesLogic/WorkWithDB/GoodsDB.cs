using DataBaseContext;
using RentaBusinesLogic.Interfaces;
using RentaBusinesLogic.Models.Good;
using RentaBusinesLogic.Services.AutoMapper;
using RentaBusinesLogic.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaBusinesLogic.WorkWithDB
{
    public class GoodsDB : IRepository<Good>
    {
        private GoodsConverter goodConverter;
        readonly Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment;

        public GoodsDB()
        {
            goodConverter = new GoodsConverter();
            mapAppartment = new Mapper<Apartment, DataBaseContext.Models.Good.Apartment>();
        }

        public async Task<Good> Read(int id)
        {
            using (WorkWith goods = new WorkWith())
            {
                DataBaseContext.Models.Good.Good good =
                    await Task.Run(() => goods.Goods.Read(id));

                Good convertedGood = goodConverter.ConvertGoodFrom(good, mapAppartment);

                return convertedGood;
            }
        }

        public async Task<IEnumerable<Good>> ReadAll()
        {
            using (WorkWith goods = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Good.Good> goodsList =
                    await Task.Run(() => goods.Goods.ReadAll());

                IEnumerable<Good> convertedGoods =
                    goodConverter.ConvertGoodsFrom(goodsList, mapAppartment);

                return convertedGoods;
            }
        }

        public async Task<Good> Create(Good item)
        {
            using (WorkWith goods = new WorkWith())
            {
                DataBaseContext.Models.Good.Good convertedGood =
                    goodConverter.ConvertGoodTo(item, mapAppartment);

                await Task.Run(() => goods.Goods.Create(convertedGood));

                return item;
            }
        }

        public async Task<Good> Update(Good item)
        {
            using (WorkWith goods = new WorkWith())
            {
                DataBaseContext.Models.Good.Good convertedGood =
                    goodConverter.ConvertGoodTo(item, mapAppartment);

                await Task.Run(() => goods.Goods.Update(convertedGood));

                return item;
            }
        }

        public async Task Delete(int id)
        {
            using (WorkWith goods = new WorkWith())
            {
                await Task.Run(() => goods.Goods.Delete(id));
            }
        }
    }
}
