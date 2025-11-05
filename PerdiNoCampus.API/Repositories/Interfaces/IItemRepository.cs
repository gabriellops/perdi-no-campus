using PerdiNoCampus.API.Models;
using System.Linq.Expressions;

namespace PerdiNoCampus.API.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task AddAsync(ItemModel itemModel);
        Task<List<ItemModel>> ListAsync();
        Task<List<ItemModel>> ListAsync(Expression<Func<ItemModel, bool>> expression);
        Task<ItemModel> FindAsync(int id);
        Task<ItemModel> FindAsNoTrackingAsync(Expression<Func<ItemModel, bool>> expression);
        Task EditAsync(ItemModel itemModel);
        Task DeleteAsync(int id);
    }
}
