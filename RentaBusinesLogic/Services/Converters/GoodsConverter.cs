using RentaBusinesLogic.Models.Good;
using RentaBusinesLogic.Services.AutoMapper;
using System.Collections.Generic;

namespace RentaBusinesLogic.Services.Converters
{
    public class GoodsConverter
    {
        public Good ConvertGoodFrom(DataBaseContext.Models.Good.Good good,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapApartment)
        {
            Good convertedGood = new Good()
            {
                Id = good.Id,
                DateOfAdd = good.DateOfAdd,
                DateOfUpdate = good.DateOfUpdate,
                Watchings = good.Watchings,
                Link = good.Link,
                Describe = good.Describe,
                Price = good.Price,
                Status = good.Status,
                Apartment = mapApartment.ToResource.Map<Apartment>(good.Apartment)
            };

            return convertedGood;
        }

        public IEnumerable<Good> ConvertGoodsFrom(
            IEnumerable<DataBaseContext.Models.Good.Good> goods,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapApartment)
        {
            IEnumerable<Good> convertedGoodsList = new List<Good>();

            foreach (var item in goods)
            {
                (convertedGoodsList as List<Good>)
                    .Add(ConvertGoodFrom(item, mapApartment));
            }

            return convertedGoodsList;
        }

        public DataBaseContext.Models.Good.Good ConvertGoodTo(Good good,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapApartment)
        {
            DataBaseContext.Models.Good.Good convertedGood
                = new DataBaseContext.Models.Good.Good()
                {
                    Id = good.Id,
                    DateOfAdd = good.DateOfAdd,
                    DateOfUpdate = good.DateOfUpdate,
                    Watchings = good.Watchings,
                    Link = good.Link,
                    Describe = good.Describe,
                    Price = good.Price,
                    Status = good.Status,
                    Apartment = mapApartment.ToResource
                    .Map<DataBaseContext.Models.Good.Apartment>(good.Apartment)
                };

            return convertedGood;
        }
    }
}
