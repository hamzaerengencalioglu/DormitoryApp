using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRoomAsync(Room room)
        {
            if (room.RoomNumber <= 0)
                throw new ArgumentException("Room number cannot be less than or equal to 0");

            if (room.RoomCapacity < 0)
                throw new ArgumentException("Room capacity cannot be less than 0");

            await _unitOfWork.Repository<Room>().CreateAsync(room);

        }

        public async Task DeleteRoomAsync(int RoomId)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(RoomId);
            if (room == null)
                throw new ArgumentException("No room found to be deleted");

            await _unitOfWork.Repository<Room>().DeleteAsync(RoomId);
        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            return await _unitOfWork.Repository<Room>().GetAllAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int RoomId)
        {
            return await _unitOfWork.Repository<Room>().GetByIdAsync(RoomId);
        }

        public async Task UpdateRoomAsync(Room room)
        {
            if (room == null)
                throw new ArgumentNullException("No room found to be updated");
            
            if (room.RoomNumber <= 0)
                throw new ArgumentException("Room number cannot be less than or equal to 0");

            if (room.RoomCapacity < 0)
                throw new ArgumentException("Room capacity cannot be less than 0");

            await _unitOfWork.Repository<Room>().UpdateAsync(room);

        }
    }
}
