using RentaAdmin.Models.Good;
using RentaAdmin.Services.AutoMapper;
using RentaAdmin.Services.Converters;
using RentaBusinesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAdmin.Services
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
