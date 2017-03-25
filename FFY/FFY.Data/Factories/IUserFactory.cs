using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string username,
            string firstName,
            string lastName,
            string email);
    }
}
