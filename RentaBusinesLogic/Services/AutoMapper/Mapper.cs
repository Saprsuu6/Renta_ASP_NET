using AutoMapper;

namespace RentaBusinesLogic.Services.AutoMapper
{
    public class Mapper<TResource, TDestiantion>
    {
        public Mapper ToDestiantion
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<TResource, TDestiantion>());

                return new Mapper(config);
            }
        }

        public Mapper ToResource
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<TDestiantion, TResource>());

                return new Mapper(config);
            }
        }
    }
}
