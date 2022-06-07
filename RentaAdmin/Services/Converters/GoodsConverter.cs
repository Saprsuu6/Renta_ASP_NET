using RentaAdmin.Models.Good;
using RentaAdmin.Services.AutoMapper;

namespace RentaAdmin.Services.Converters
{
    public class GoodsConverter
    {
        public Good ConvertGoodFrom(RentaBusinesLogic.Models.Good.Good good,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
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
                Apartment = mapAppartment.ToResource.Map<Apartment>(good.Apartment)
            };

            return convertedGood;
        }
    }
}
