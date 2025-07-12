using YurtApps.Application.DTOs.RoomDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRoomAsync(CreateRoomDto dto, string UserId)
        {
            if (dto.RoomNumber <= 0)
                throw new ArgumentException("Room number cannot be less than or equal to 0");

            if (dto.RoomCapacity < 0)
                throw new ArgumentException("Room capacity cannot be less than 0");

            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dto.DormitoryId);
            if (dormitory == null)
                throw new ArgumentException("Dormitory not found.");

            if (dormitory.UserId != UserId)
                throw new UnauthorizedAccessException("You do not have permission to add rooms to this dormitory.");

            var entity = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomCapacity = dto.RoomCapacity,
                DormitoryId = dto.DormitoryId
            };

            await _unitOfWork.Repository<Room>().CreateAsync(entity);


            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRoomAsync(int RoomId)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(RoomId);

            if (room == null)
                throw new ArgumentException("No room found to be deleted");

            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(room.DormitoryId);

            await _unitOfWork.Repository<Room>().DeleteAsync(RoomId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultRoomDto>> GetAllRoomAsync(string userId)
        {
            var dormitories = await _unitOfWork.Repository<Dormitory>().GetAllAsync();
            var userDormitoryIds = dormitories
                .Where(d => d.UserId == userId)
                .Select(d => d.DormitoryId)
                .ToList();

            var rooms = await _unitOfWork.Repository<Room>().GetAllAsync();
            var result = rooms
                .Where(r => userDormitoryIds.Contains(r.DormitoryId))
                .Select(r => new ResultRoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomCapacity = r.RoomCapacity,
                    DormitoryId = r.DormitoryId
                })
                .ToList();

            return result;
        }

        public async Task<List<ResultRoomDto>> GetRoomByDormitoryIdAsync(int DormitoryId, string UserId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(DormitoryId);

            if (dormitory == null)
                throw new ArgumentException("Dormitory not found");

            if (dormitory.UserId != UserId)
                throw new UnauthorizedAccessException("You do not own this dormitory.");

            var rooms = await _unitOfWork.Repository<Room>().GetAllAsync();
            return rooms
                .Where(r => r.DormitoryId == DormitoryId)
                .Select(r => new ResultRoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomCapacity = r.RoomCapacity,
                    DormitoryId = r.DormitoryId
                })
                .ToList();
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
                RoomNumber = dto.RoomNumber,
                RoomCapacity = dto.RoomCapacity
            };

            await _unitOfWork.Repository<Room>().UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}