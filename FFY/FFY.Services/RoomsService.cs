using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;

namespace FFY.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly IFFYData data;

        public RoomsService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }


        public void AddRoom(Room room)
        {
            Guard.WhenArgument<Room>(room, "Room cannot be null.")
                .IsNull()
                .Throw();

            this.data.RoomsRepository.Add(room);
            this.data.SaveChanges();
        }

        public Room GetRoomById(int id)
        {
            return this.data.RoomsRepository.GetById(id);
        }

        public IEnumerable<Room> GetRooms()
        {
            return this.data.RoomsRepository.All().ToList();
        }
    }
}
