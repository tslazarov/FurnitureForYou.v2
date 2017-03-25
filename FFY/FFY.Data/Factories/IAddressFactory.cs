using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IAddressFactory
    {
        Address CreateAddress(string street, string city, string country);
    }
}
