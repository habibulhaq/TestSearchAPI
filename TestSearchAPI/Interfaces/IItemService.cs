using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
#pragma warning disable
namespace TestSearchAPI.Interfaces
{
    public interface IItemService
    {
        Task<ApiResponse<object>> SearchItems(SearchCriteriaDTO payload, int userId);
        Task<ApiResponse<object>> GetSearchHistoryAsync(int userId);
        Task AddSearchHistoryAsync(SearchCriteriaDTO payload, int userId);
    }
}
