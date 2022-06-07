using RentaAppication.Models.Good;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using RentaBusinesLogic;
using System.Threading.Tasks;

namespace RentaAppication.Services
{
    public class GetGood
    {
        private WorkWithContexts contexts;

        public GetGood()
        {
            contexts = new WorkWithContexts();
        }

        public async Task<Good> Read(int id, GoodsConverter goodsConverter,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        {
            RentaBusinesLogic.Models.Good.Good good
                = await contexts.Goods.Read(id);

            Good converedGood
                = goodsConverter.ConvertGoodFrom(good, mapApartment);

            return converedGood;
        }
    }
}
