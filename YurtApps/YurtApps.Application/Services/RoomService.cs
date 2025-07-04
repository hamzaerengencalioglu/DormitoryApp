using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Application.DTOs.RoomDTOs;
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

        public async Task CreateRoomAsync(CreateRoomDto dto)
        {
            if (dto.RoomNumber <= 0)
                throw new ArgumentException("Room number cannot be less than or equal to 0");

            if (dto.RoomCapacity < 0)
                throw new ArgumentException("Room capacity cannot be less than 0");
            var entity = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomCapacity = dto.RoomCapacity
            };

            await _unitOfWork.Repository<Room>().CreateAsync(entity);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteRoomAsync(int RoomId)
        {
            var entity = await _unitOfWork.Repository<Room>().GetByIdAsync(RoomId);
            if (entity == null)
                throw new ArgumentException("No room found to be deleted");

            await _unitOfWork.Repository<Room>().DeleteAsync(RoomId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultRoomDto>> GetAllRoomAsync()
        {
            var list = await _unitOfWork.Repository<Room>().GetAllAsync();
            return list.Select(r => new ResultRoomDto
            { 
                RoomId = r.RoomId,
                RoomNumber=r.RoomNumber,
                RoomCapacity=r.RoomCapacity
            }).ToList();
        }

        public async Task<ResultRoomDto> GetRoomByIdAsync(int RoomId)
        {
            var entity = await _unitOfWork.Repository<Room>().GetByIdAsync(RoomId);
            if (entity == null)
                return null;

            return new ResultRoomDto
            {
                RoomId = entity.RoomId,
                RoomNumber = entity.RoomNumber,
                RoomCapacity = entity.RoomCapacity
            };
        }

        public async Task UpdateRoomAsync(UpdateRoomDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException("No room found to be updated");
            
            if (dto.RoomNumber <= 0)
                throw new ArgumentException("Room number cannot be less than or equal to 0");

            if (dto.RoomCapacity < 0)
                throw new ArgumentException("Room capacity cannot be less than 0");

            var entity = new Room
            {
                RoomId = dto.RoomId,
                RoomNumber = dto.RoomNumber,
                RoomCapacity = dto.RoomCapacity
            };

            await _unitOfWork.Repository<Room>().UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

        }
    }
}
