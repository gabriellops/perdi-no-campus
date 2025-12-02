using PerdiNoCampus.API.Models;
using PerdiNoCampus.API.Repositories.Interfaces;
using Supabase;
using System.Linq.Expressions;

namespace PerdiNoCampus.API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Client _client;

        public ItemRepository(Client client)
        {
            _client = client;
        }

        public async Task AddAsync(ItemModel item)
        {
            await _client.From<ItemModel>().Insert(item);
        }

        public async Task<List<ItemModel>> ListAsync()
        {
            var response = await _client.From<ItemModel>().Get();
            return response.Models;
        }

        public async Task<List<ItemModel>> ListAsync(Expression<Func<ItemModel, bool>> expression)
        {
            var response = await _client.From<ItemModel>().Get();
            return response.Models.AsQueryable().Where(expression).ToList();
        }

        public async Task<ItemModel> FindAsync(Guid id)
        {
            var response = await _client
                .From<ItemModel>()
                .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, id.ToString())
                .Single();

            return response;
        }

        public async Task<ItemModel> FindAsNoTrackingAsync(Expression<Func<ItemModel, bool>> expression)
        {
            var response = await _client.From<ItemModel>().Get();
            return response.Models.AsQueryable().FirstOrDefault(expression);
        }

        public async Task EditAsync(ItemModel item)
        {
            await _client.From<ItemModel>().Update(item);
        }
    }
}
