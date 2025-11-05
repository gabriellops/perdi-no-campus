using PerdiNoCampus.API.Models;
using System.Linq.Expressions;

namespace PerdiNoCampus.API.Services.Interfaces
{
    public interface IItemService
    {
        Task CriarAsync(ItemModel itemModel);
        Task<List<ItemModel>> ObterTodosAsync();
        Task<List<ItemModel>> ObterTodosAsync(Expression<Func<ItemModel, bool>> expression);
        Task<ItemModel> ObterPorIdAsync(int id);
        Task AtualizarAsync(ItemModel itemModel);
        Task DeletarAsync(int id);
    }
}
