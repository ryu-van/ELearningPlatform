using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Repositories;

namespace E_learning_platform.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository branchRepository;
        private readonly IMapper mapper;

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            this.branchRepository = branchRepository;
            this.mapper = mapper;
        }

        public Task<bool> ChangeBranchStatusAsync(long branchId, bool isActive)
        {
            return branchRepository.ChangeStatus(branchId, isActive);
        }

        public async Task<BranchResponse> CreateNewBranchAsync(BranchRequest request)
        {
            var newRole = await branchRepository.CreateBranchAsync(request);
            return mapper.Map<BranchResponse>(newRole);
        }

        public Task<bool> DeleteBranchAsync(long branchId)
        {
           return branchRepository.DeleteBranchAsync(branchId);
        }

        public async Task<BranchResponse> GetBranchById(long branchId)
        {
            var existingBranch = await branchRepository.GetBranchById(branchId);
            return mapper.Map<BranchResponse>(existingBranch);
        }

        public async Task<PagedResponse<BranchResponse>> GetPageOfBranchAsync(string? keyword, bool? isActive, int page, int pageSize)
        {
            var pageBranch = await branchRepository.GetPageOfBranchAsync(keyword, isActive, page, pageSize);

            var mappedData = mapper.Map<IEnumerable<BranchResponse>>(pageBranch.Data);

            return new PagedResponse<BranchResponse>(
                mappedData,
                pageBranch.TotalItems,
                pageBranch.CurrentPage,
                pageBranch.PageSize
            );
        }

        public async Task<BranchResponse> UpdateBranch(long branchId, BranchRequest request)
        {
            var changedBranch = await branchRepository.UpdateBranchAsync(branchId, request);
            return mapper.Map<BranchResponse>(changedBranch);


        }
    }
}
