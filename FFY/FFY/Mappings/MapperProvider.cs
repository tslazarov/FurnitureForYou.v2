using AutoMapper;

namespace FFY.Web.Mappings
{
    public class MapperProvider : IMapperProvider
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}