using FFY.Data.Contracts;
using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using Bytes2you.Validation;

namespace FFY.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IFFYData data;

        public AddressesService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public void AddAddress(Address address)
        {
            Guard.WhenArgument<Address>(address, "Address cannot be null.")
                .IsNull()
                .Throw();

            this.data.AddressesRepository.Add(address);
            this.data.SaveChanges();
        }
    }
}
