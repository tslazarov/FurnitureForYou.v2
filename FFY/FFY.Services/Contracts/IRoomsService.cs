using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface IRoomsService
    {
        void AddRoom(Room room);

        Room GetRoomById(int id);

        IEnumerable<Room> GetRooms();
    }
}
