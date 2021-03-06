﻿using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;

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

        public Address GetAddressById(int id)
        {
            return this.data.AddressesRepository.GetById(id);
        }

        public void AddAddress(Address address)
        {
            Guard.WhenArgument<Address>(address, "Address cannot be null.")
                .IsNull()
                .Throw();

            this.data.AddressesRepository.Add(address);
            this.data.SaveChanges();
        }

        public void DeleteAddress(Address address)
        {
            Guard.WhenArgument<Address>(address, "Address cannot be null.")
                .IsNull()
                .Throw();

            this.data.AddressesRepository.Delete(address);
            this.data.SaveChanges();
        }
    }
}
