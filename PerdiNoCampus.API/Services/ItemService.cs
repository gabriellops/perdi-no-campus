using PerdiNoCampus.API.Contracts;
using PerdiNoCampus.API.Models;
using PerdiNoCampus.API.Repositories;
using PerdiNoCampus.API.Repositories.Interfaces;
using PerdiNoCampus.API.Services.Interfaces;
using System.Linq.Expressions;

namespace PerdiNoCampus.API.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarAsync(ItemModel item)
        {
            item.CriadoEm = DateTime.Now;
            item.Ativo = true;
            item.FoiRecuperado = item.FoiRecuperado ?? false;

            await _repository.AddAsync(item);
        }

        public async Task<List<ItemModel>> ObterTodosAsync()
        {
            return await _repository.ListAsync(x => x.Ativo && x.FoiRecuperado == false);
        }

        public async Task<List<ItemModel>> ObterTodosAsync(Expression<Func<ItemModel, bool>> expression)
        {
            var items = await _repository.ListAsync(expression);

            return items ?? new List<ItemModel>();
        }

        //public async Task<ItemModel> ObterPorIdAsync(Guid id)
        //{
        //    var item =  await _repository.FindAsync(id);
        //    if (item == null)
        //    {
        //        throw new Exception("Item não encontrado.");
        //    }

        //    return item;
        //}

        public async Task AtualizarAsync(ItemModel item)
        {
            var itemExistente = await _repository.FindAsNoTrackingAsync(x => x.Id == item.Id && x.Ativo && x.FoiRecuperado == false);
            if (itemExistente == null)
            {
                throw new Exception("Item não encontrado ou já foi recuperado.");
            }
            item.CriadoEm = itemExistente.CriadoEm;

            await _repository.EditAsync(item);
        }

        public async Task DeletarAsync(Guid id)
        {
            var itemExistente = await _repository.FindAsync(id);
            if (itemExistente == null)
            {
                throw new Exception("Item não encontrado.");
            }

            itemExistente.Ativo = false;
            await _repository.EditAsync(itemExistente);
        }
    }
}
