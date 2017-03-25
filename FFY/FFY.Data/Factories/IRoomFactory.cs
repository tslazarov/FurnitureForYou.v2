using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IRoomFactory
    {
        Room CreateRoom(string name, string imagePath);
    }
}
