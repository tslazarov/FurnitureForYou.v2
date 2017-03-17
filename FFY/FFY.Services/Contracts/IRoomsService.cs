using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IRoomsService
    {
        void AddRoom(Room room);

        Room GetRoomById(int id);

        IEnumerable<Room> GetRooms();
    }
}
