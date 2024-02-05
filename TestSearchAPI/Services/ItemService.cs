using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
#pragma warning disable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Domain;
using TestSearchAPI.Helper;
using TestSearchAPI.Interfaces;


namespace TestSearchAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly TestSearchDBContext _dbContext;
        public ItemService(TestSearchDBContext dbContext)
        {
            this._dbContext = dbContext;

        }
        public async Task<ApiResponse<object>> SearchItems(SearchCriteriaDTO payload,int userId)
        {
            try
            {
                BackgroundJob.Enqueue(() => AddSearchHistoryAsync(payload, userId));
                var query =  _dbContext.Items
                                .Where(x => payload.ItemName == null || x.ItemName.Contains(payload.ItemName))
                                .Where(x => payload.ItemDescription == null || x.ItemDescription.Contains(payload.ItemDescription))
                                .Where(x => payload.Quantity == null || x.Quantity == payload.Quantity)
                                .Where(x => payload.Liked == null || x.Liked == payload.Liked)
                                .Where(x => payload.CreatedOn == null || x.CreatedOn == payload.CreatedOn);
                if (!string.IsNullOrEmpty(payload.SortBy))
                {
                    switch (payload.SortBy.ToLowerInvariant())
                    {
                        case "itemname":
                            query = payload.SortType == 0 ? query.OrderBy(x => x.ItemName) : query.OrderByDescending(x => x.ItemName);
                            break;
                        case "itemdescription":
                            query = payload.SortType == 0 ? query.OrderBy(x => x.ItemDescription) : query.OrderByDescending(x => x.ItemDescription);
                            break;
                        case "quantity":
                            query = payload.SortType == 0 ? query.OrderBy(x => x.Quantity) : query.OrderByDescending(x => x.Quantity);
                            break;
                        case "liked":
                            query = payload.SortType == 0 ? query.OrderBy(x => x.Liked) : query.OrderByDescending(x => x.Liked);
                            break;
                        case "createdOn":
                            query = payload.SortType == 0 ? query.OrderBy(x => x.CreatedOn) : query.OrderByDescending(x => x.CreatedOn);
                            break;
                        default:
                            query = payload.SortType == 0 ? query.OrderBy(x => x.ItemName) : query.OrderByDescending(x => x.ItemName);
                            break;
                    }
                }
                
                var res = await query.AsNoTracking().ToListAsync();
                if (res != null && res.Any())
                {
                    return new ApiResponse<object> { HasError = false, Message = "", Data = res, Count = res.Count };
                }
                return new ApiResponse<object> { HasError = false, Message = ErrorMessagesContants.NoMatchFound, Count = 0 };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { HasError = true, Message = $"Error occured:{ex.Message}" };
            }
        }

        public async Task<ApiResponse<object>> GetSearchHistoryAsync(int userId)
        {

            var query =  _dbContext.SearchHistories
                .Where(sh => sh.UserId == userId)
                .OrderByDescending(sh => sh.SearchedOn);
            var res = await query.AsNoTracking().ToListAsync();
            if (res != null && res.Any())
            {
                return new ApiResponse<object> { HasError = false, Message = "", Data = res, Count = res.Count };
            }
            return new ApiResponse<object> { HasError = false, Message = ErrorMessagesContants.NoMatchFound, Count = 0 };
        }

        public async Task AddSearchHistoryAsync(SearchCriteriaDTO payload, int userId)
        {
            string jsonString = JsonConvert.SerializeObject(payload);
            
            _dbContext.SearchHistories.Add(new SearchHistory
            {
                Query = jsonString,
                SearchedOn = DateTime.UtcNow,
                UserId = userId
            });

            await _dbContext.SaveChangesAsync();
        }

    }
}
