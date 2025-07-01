using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.Entities;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateRoomAsync(Room room)
        {
            _appDbContext.Room.Add(room);
            await _appDbContext.SaveChangesAsync();
        }
            
        public async Task DeleteRoomAsync(int RoomId)
        {
            var room = await GetRoomByIdAsync(RoomId);
            if (room != null) 
            {
                _appDbContext.Room.Remove(room);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            var list = await _appDbContext.Room.ToListAsync();
            return list;
        }

        public async Task<Room> GetRoomByIdAsync(int RoomId)
        {
            var room = await _appDbContext.Room.Where(r => r.RoomId == RoomId)
                .FirstOrDefaultAsync();
            return room;
        }
        
        public async Task UpdateRoomAsync(Room room)
        {
            _appDbContext.Entry(room).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}