using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace E_learning_platform.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext _context;
        public LevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ChangeStatusAsync(long id, bool status)
        {
            var entity = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Level", new[] { id });
            entity.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Level> CreateAsync(LevelRequest request)
        {
            var entity = new Level { Name = request.Name, Code = request.Code, Description = request.Description, Order = request.Order, IsActive = request.IsActive };
            _context.Levels.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Level", new[] { id });
            _context.Levels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Level> GetByIdAsync(long id)
        {
            var entity = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Level", new[] { id });
            return entity;
        }
        public async Task<PagedResponse<Level>> GetPagedAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Levels.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(l => (l.Name != null && l.Name.Contains(keyword)) || (l.Description != null && l.Description.Contains(keyword)));
            }
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }
            var total = await query.CountAsync();
            var items = await query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<Level>(items, total, page, pageSize);
        }
        public async Task<Level> UpdateAsync(long id, LevelRequest request)
        {
            var entity = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Level", new[] { id });
            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.Description = request.Description;
            entity.Order = request.Order;
            entity.IsActive = request.IsActive;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
