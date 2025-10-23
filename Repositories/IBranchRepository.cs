using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Repositories
{
    public interface IBranchRepository
    {
        Task<PagedResponse<Branch>> GetPagedBranchAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<Branch> GetBranchById(long branchId);
        Task<Branch> CreateBranchAsync(BranchRequest request);
        Task<Branch> UpdateBranchAsync(long branchId, BranchRequest request);
        Task<bool> DeleteBranchAsync(long branchId);

        Task<bool> ChangeStatus(long branchId,bool status);


       
    }
}
