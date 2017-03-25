namespace FFY.Web.Mappings
{
    public interface IMapperProvider
    {
        TDestination Map<TDestination>(object source);
    }
}