using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Data.Contracts
{
    public interface IFFYData
    {
        IEfRepository<Address> AddressesRepository { get; }

        IEfRepository<Category> CategoriesRepository { get; }

        IEfRepository<Contact> ContactsRepository { get; }

        IEfRepository<Order> OrdersRepository { get; }

        IEfRepository<Room> RoomsRepository { get; }

        IEfRepository<ShoppingCart> ShoppingCartsRepository { get; }

        IEfRepository<CartProduct> CartProductsRepository { get; }

        IEfRepository<User> UsersRepository { get; }

        IDeletableEfRepository<Product> ProductsRepository { get; }

        void SaveChanges();

    }
}
