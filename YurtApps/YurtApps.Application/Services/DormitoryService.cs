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
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DormitoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDormitoryAsync(Dormitory dormitory)
        {
            if (string.IsNullOrWhiteSpace(dormitory.DormitoryName))
                throw new ArgumentException("Dormitory name cannot be empty.");

            if (dormitory.DormitoryCapacity < 0)
                throw new ArgumentException("Dormitory capacity cannot be less than 0.");

            if (string.IsNullOrWhiteSpace(dormitory.DormitoryAddress))
                throw new ArgumentException("Dormitory address cannot be empty.");

            await _unitOfWork.Repository<Dormitory>().CreateAsync(dormitory);

        }

        public async Task DeleteDormitoryAsync(int DormitoryId)
        {
            var dormitory = await _unitOfWork.Repository<Dormitory>().GetByIdAsync(DormitoryId);

            if (dormitory == null)
                throw new ArgumentException("No dormitory found to be deleted");

            await _unitOfWork.Repository<Dormitory>().DeleteAsync(DormitoryId);
        }

        public async Task<List<Dormitory>> GetAllDormitoryAsync()
        {
            return await _unitOfWork.Repository<Dormitory>().GetAllAsync();
        }

        public async Task<Dormitory> GetDormitoryByIdAsync(int DormitoryId)
        {
            return await _unitOfWork.Repository<Dormitory>().GetByIdAsync(DormitoryId);
        }

        public async Task UpdateDormitoryAsync(Dormitory dormitory)
        {
            if (dormitory == null)
                throw new ArgumentException("No dormitory found to be updated");

            if (string.IsNullOrWhiteSpace(dormitory.DormitoryName))
                throw new ArgumentException("Dormitory name cannot be empty.");

            if (dormitory.DormitoryCapacity < 0)
                throw new ArgumentException("Dormitory name cannot be less than 0.");

            if (string.IsNullOrWhiteSpace(dormitory.DormitoryAddress))
                throw new ArgumentException("Dormitory name cannot be empty.");

            await _unitOfWork.Repository<Dormitory>().UpdateAsync(dormitory);
        }
    }
}
