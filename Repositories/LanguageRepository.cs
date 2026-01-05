using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;
namespace E_learning_platform.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ChangeStatusAsync(long id, bool status)
        {
            var entity = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Language", new[] { id });
            entity.IsActive = status;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Language> CreateAsync(LanguageRequest request)
        {
            var entity = new Language { Name = request.Name, Code = request.Code, IsActive = request.IsActive };
            _context.Languages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Language", new[] { id });
            _context.Languages.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Language> GetByIdAsync(long id)
        {
            var entity = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Language", new[] { id });
            return entity;
        }
        public async Task<PagedResponse<Language>> GetPagedAsync(string keyword, bool? isActive, int page, int pageSize)
        {
            var query = _context.Languages.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{keyword}%"));
            }
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }
            var total = await query.CountAsync();
            var items = await query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<Language>(items, total, page, pageSize);
        }
        public async Task<Language> UpdateAsync(long id, LanguageRequest request)
        {
            var entity = await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("Language", new[] { id });
            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.IsActive = request.IsActive;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
