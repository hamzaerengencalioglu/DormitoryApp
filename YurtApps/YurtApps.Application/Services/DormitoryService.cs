using YurtApps.Application.DTOs.DormitoryDTOs;
using YurtApps.Application.Interfaces;
using YurtApps.Domain.Entities;

namespace YurtApps.Application.Services
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DormitoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDormitoryAsync(CreateDormitoryDto dto, string UserId)
        {
            if (string.IsNullOrWhiteSpace(dto.DormitoryName))
                throw new ArgumentException("Dormitory name cannot be empty.");

            if (dto.DormitoryCapacity < 0)
                throw new ArgumentException("Dormitory capacity cannot be less than 0.");

            if (string.IsNullOrWhiteSpace(dto.DormitoryAddress))
                throw new ArgumentException("Dormitory address cannot be empty.");

            var entity = new Dormitory
            {
                DormitoryName = dto.DormitoryName,
                DormitoryCapacity = dto.DormitoryCapacity,
                DormitoryAddress = dto.DormitoryAddress,
                UserId = UserId
            };

            await _unitOfWork.Repository<Dormitory>().CreateAsync(entity);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteDormitoryAsync(int DormitoryId, string UserId)
        {
            var entity = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(DormitoryId);

            if (entity == null)
                throw new ArgumentException("No dormitory found to be deleted");

            if (entity.UserId != UserId)
                throw new UnauthorizedAccessException("You do not have permission to delete this dormitory");

            await _unitOfWork.Repository<Dormitory>().DeleteAsync(DormitoryId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ResultDormitoryDto>> GetAllDormitoryAsync(string UserId)
        {
            var list = await _unitOfWork.Repository<Dormitory>().GetAllAsync();
            return list
                .Where(u => u.UserId == UserId)
                .Select(d => new ResultDormitoryDto
            {
                DormitoryId = d.DormitoryId,
                DormitoryName = d.DormitoryName,
                DormitoryCapacity = d.DormitoryCapacity,
                DormitoryAddress = d.DormitoryAddress
            }).ToList();

        }

        public async Task<ResultDormitoryDto> GetDormitoryByIdAsync(int DormitoryId)
        {
            var entity = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(DormitoryId);
            if (entity == null) 
                return null;

                return new ResultDormitoryDto
                {
                    DormitoryId = entity.DormitoryId,
                    DormitoryName= entity.DormitoryName,
                    DormitoryCapacity= entity.DormitoryCapacity,
                    DormitoryAddress= entity.DormitoryAddress
                };
        }

        public async Task UpdateDormitoryAsync(UpdateDormitoryDto dto, string userId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dto.DormitoryId);

            if (dormitory == null)
                throw new ArgumentException("No dormitory found to be updated");

            if (dormitory.UserId != userId)
                throw new UnauthorizedAccessException("You don't have access to this dormitory");

            if (string.IsNullOrWhiteSpace(dto.DormitoryName))
                throw new ArgumentException("Dormitory name cannot be empty.");

            if (dto.DormitoryCapacity < 0)
                throw new ArgumentException("Dormitory capacity cannot be less than 0.");

            if (string.IsNullOrWhiteSpace(dto.DormitoryAddress))
                throw new ArgumentException("Dormitory address cannot be empty.");

            dormitory.DormitoryName = dto.DormitoryName;
            dormitory.DormitoryCapacity = dto.DormitoryCapacity;
            dormitory.DormitoryAddress = dto.DormitoryAddress;

            await _unitOfWork.CommitAsync();
        }


        public async Task<bool> UserOwnsDormitory(string userId, int dormitoryId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(dormitoryId);

            if (dormitory == null)
                return false;

            return dormitory.UserId == userId;
        }

    }
}
