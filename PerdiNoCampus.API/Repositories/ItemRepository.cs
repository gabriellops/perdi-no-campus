using PerdiNoCampus.API.Models;
using PerdiNoCampus.API.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PerdiNoCampus.API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly PerdiNoCampusContext _context;

        public ItemRepository(PerdiNoCampusContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ItemModel item)
        {
            await _context.Set<ItemModel>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemModel>> ListAsync()
        {
            return await _context.Set<ItemModel>().ToListAsync();
        }

        public async Task<List<ItemModel>> ListAsync(Expression<Func<ItemModel, bool>> expression)
        {
            return await _context.Set<ItemModel>().Where(expression).ToListAsync();
        }

        public async Task<ItemModel> FindAsync(int id)
        {
            return await _context.Set<ItemModel>().FindAsync(id);
        }

        public async Task<ItemModel> FindAsNoTrackingAsync(Expression<Func<ItemModel, bool>> expression)
        {
            return await _context.Set<ItemModel>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task EditAsync(ItemModel item)
        {
            _context.Set<ItemModel>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ItemModel item)
        {
            _context.Set<ItemModel>().Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
