using FFY.Models;

namespace FFY.Services.Contracts
{
    public interface IAddressesService
    {
        Address GetAddressById(int id);

        void AddAddress(Address address);

        void DeleteAddress(Address address);
    }
}
