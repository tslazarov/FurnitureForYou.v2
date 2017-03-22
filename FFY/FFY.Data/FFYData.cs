using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;

namespace FFY.Data
{
    public class FFYData : IFFYData
    {
        private readonly IFFYDbContext dbContext;
        private readonly IEfRepository<Address> addressesRepository;
        private readonly IEfRepository<Category> categoriesRepository;
        private readonly IEfRepository<Contact> contactsRepository;
        private readonly IEfRepository<Order> ordersRepository;
        private readonly IEfRepository<Room> roomsRepository;
        private readonly IEfRepository<ShoppingCart> shoppingCartsRepository;
        private readonly IEfRepository<CartProduct> cartProductsRepository;
        private readonly IEfRepository<User> usersRepository;
        private readonly IEfRepository<ChatUser> chatUsersRepository;
        private readonly IDeletableEfRepository<Product> productsRepository;

        public FFYData(IFFYDbContext dbContext,
            IEfRepository<Address> addressesRepository,
            IEfRepository<Category> categoriesRepository,
            IEfRepository<Contact> contactsRepository,
            IEfRepository<Order> ordersRepository,
            IEfRepository<Room> roomsRepository,
            IEfRepository<ShoppingCart> shoppingCartsRepository,
            IEfRepository<CartProduct> cartProductsRepository,
            IEfRepository<User> usersRepository,
            IEfRepository<ChatUser> chatUsersRepository,
            IDeletableEfRepository<Product> productsRepository)
        {
            Guard.WhenArgument<IFFYDbContext>(dbContext, "Database context cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<Address>>(addressesRepository, "Addresses repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<Category>>(categoriesRepository, "Categories repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<Contact>>(contactsRepository, "Contacts repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<Order>>(ordersRepository, "Orders repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<Room>>(roomsRepository, "Rooms repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<ShoppingCart>>(shoppingCartsRepository, "Shopping carts repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<CartProduct>>(cartProductsRepository, "Cart products repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<User>>(usersRepository, "Users repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IEfRepository<ChatUser>>(chatUsersRepository, "Chat users repository cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDeletableEfRepository<Product>>(productsRepository, "Products repository cannot be null.")
                .IsNull()
                .Throw();

            this.dbContext = dbContext;
            this.addressesRepository = addressesRepository;
            this.categoriesRepository = categoriesRepository;
            this.contactsRepository = contactsRepository;
            this.ordersRepository = ordersRepository;
            this.roomsRepository = roomsRepository;
            this.shoppingCartsRepository = shoppingCartsRepository;
            this.cartProductsRepository = cartProductsRepository;
            this.usersRepository = usersRepository;
            this.chatUsersRepository = chatUsersRepository;
            this.productsRepository = productsRepository;
        }

        public IEfRepository<Address> AddressesRepository
        {
            get
            {
                return this.addressesRepository;
            }
        }

        public IEfRepository<Category> CategoriesRepository
        {
            get
            {
                return this.categoriesRepository;
            }
        }

        public IEfRepository<Contact> ContactsRepository
        {
            get
            {
                return this.contactsRepository;
            }
        }

        public IEfRepository<Order> OrdersRepository
        {
            get
            {
                return this.ordersRepository;
            }
        }

        public IEfRepository<Room> RoomsRepository
        {
            get
            {
                return this.roomsRepository;
            }
        }

        public IEfRepository<ShoppingCart> ShoppingCartsRepository
        {
            get
            {
                return this.shoppingCartsRepository;
            }
        }

        public IEfRepository<CartProduct> CartProductsRepository
        {
            get
            {
                return this.cartProductsRepository;
            }
        }

        public IEfRepository<User> UsersRepository
        {
            get
            {
                return this.usersRepository;
            }
        }

        public IEfRepository<ChatUser> ChatUsersRepository
        {
            get
            {
                return this.chatUsersRepository;
            }
        }

        public IDeletableEfRepository<Product> ProductsRepository
        {
            get
            {
                return this.productsRepository;
            }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
