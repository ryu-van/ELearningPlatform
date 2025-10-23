using E_learning_platform.Data;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Exceptions;
using E_learning_platform.Helpers;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ChangeStatus(long branchId, bool status)
        {
            var existingBranch = await _context.Branches
                .Where(b => b.Id == branchId)
                .FirstOrDefaultAsync();

            if (existingBranch == null)
            {
                throw new EntityNotFoundException("Branch", new[] { branchId });
                return false;

            }
            existingBranch.IsActive = status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Branch> CreateBranchAsync(BranchRequest request)
        {
            await ValidateBranchRequestAsync(request);
            request.IsActive = true;
            var Branch = new Branch { 
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Province = request.Province,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.IsActive,
                Code = GenerateBranchCode(),
            };
            _context.Branches.Add(Branch);
            await _context.SaveChangesAsync();
            return Branch;
            

        }

        public async Task<bool> DeleteBranchAsync(long branchId)
        {
            var existingBranch = await _context.Branches
                .FirstOrDefaultAsync(b => b.Id == branchId);
            if (existingBranch == null)
            {
                throw new EntityNotFoundException("Branch", new[] { branchId });
            }
            _context.Branches.Remove(existingBranch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Branch> GetBranchById(long branchId)
        {
            var existingBranch = await _context.Branches
               .FirstOrDefaultAsync(b => b.Id == branchId);
            if (existingBranch == null)
            {
                throw new EntityNotFoundException("Branch", new[] { branchId });
            }
            return existingBranch;

        }

        public async Task<PagedResponse<Branch>> GetPagedBranchAsync(
                string keyword,
                bool? isActive,
                int page,
                int pageSize)
        {
            var query = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(b => EF.Functions.Like(b.Name, $"%{keyword}%"));
            }

            if (isActive.HasValue)
            {
                query = query.Where(b => b.IsActive == isActive.Value);
            }

            query = query.OrderBy(b => b.Id);

            return await query.ToPagedResponseAsync(page, pageSize);
        }


        public async Task<Branch> UpdateBranchAsync(long branchId, BranchRequest request)
        {
            var existingBranch = await _context.Branches
                 .FirstOrDefaultAsync(b => b.Id == branchId && !b.DeletedAt.HasValue);
            if (existingBranch == null)
            {
                throw new EntityNotFoundException("Branch", new[] { branchId });
            }
            await ValidateBranchUpdateAsync(branchId, request);
            existingBranch.Name = request.Name;
            existingBranch.Address = request.Address;
            existingBranch.City = request.City;
            existingBranch.Province = request.Province;
            existingBranch.PhoneNumber = request.PhoneNumber;
            existingBranch.IsActive = request.IsActive;
            existingBranch.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingBranch;

        }
        private string GenerateBranchCode()
        {
            return $"BRANCH-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        }
        private async Task ValidateBranchRequestAsync(BranchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Branch name is required", nameof(request.Name));
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                throw new ArgumentException("Phone number is required", nameof(request.PhoneNumber));
            }

            if (string.IsNullOrWhiteSpace(request.Address))
            {
                throw new ArgumentException("Address is required", nameof(request.Address));
            }

            var isDuplicate = await _context.Branches
                .AnyAsync(b => b.Name == request.Name && !b.DeletedAt.HasValue);

            if (isDuplicate)
            {
                throw new InvalidOperationException($"Branch with name '{request.Name}' already exists");
            }

            if (!IsValidPhoneNumber(request.PhoneNumber))
            {
                throw new ArgumentException("Invalid phone number format", nameof(request.PhoneNumber));
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                phoneNumber,
                @"^0\d{9}$"
            );
        }
        private async Task ValidateBranchUpdateAsync(long branchId, BranchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Branch name is required", nameof(request.Name));
            }

            var isDuplicate = await _context.Branches
                .AnyAsync(b => b.Name == request.Name
                            && b.Id != branchId 
                            && !b.DeletedAt.HasValue);

            if (isDuplicate)
            {
                throw new InvalidOperationException($"Branch with name '{request.Name}' already exists");
            }
        }
    }

}
