using AutoMapper;

namespace FFY.Web.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression config);
    }
}