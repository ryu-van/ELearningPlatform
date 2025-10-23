using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;

namespace E_learning_platform.Services
{
    public interface IBranchService
    {
        Task<PagedResponse<BranchResponse>> GetPageOfBranchAsync(string keyword, bool? isActive, int page, int pageSize);
        Task<BranchResponse> GetBranchById(long branchId);

        Task<BranchResponse> CreateNewBranchAsync(BranchRequest request);
        Task<BranchResponse> UpdateBranch(long branchId, BranchRequest request);

        Task<bool> DeleteBranchAsync(long branchId);

        Task<bool> ChangeBranchStatusAsync(long branchId, bool isActive);

    }
}
