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
            if (item.UsuarioId != usuarioId)
            {
                throw new UnauthorizedAccessException("Você não tem permissão para editar este item.");
            }

            item.CriadoEm = DateTime.Now;
            await _repository.AddAsync(item);
        }

        public async Task<List<ItemModel>> ObterTodosAsync()
        {
            return await _repository.ListAsync(x => x.FoiRecuperado == false);
        }

        public async Task<List<ItemModel>> ObterTodosAsync(Expression<Func<ItemModel, bool>> expression)
        {
            var items = await _repository.ListAsync(expression);
            if (items == null || items.Count == 0)
            {
                throw new Exception("Nenhum item encontrado com o nome fornecido.");
            }

            return items;
        }

        public async Task<ItemModel> ObterPorIdAsync(int id)
        {
            var item =  await _repository.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Item não encontrado.");
            }

            return item;
        }

        public async Task AtualizarAsync(ItemModel item)
        {
            var itemExistente = await _repository.FindAsNoTrackingAsync(x => x.Id == item.Id && x.FoiRecuperado == false);
            if (itemExistente == null)
            {
                throw new Exception("Item não encontrado ou já foi recuperado.");
            }

            await _repository.EditAsync(item);
        }

        public async Task DeletarAsync(int id)
        {
            var itemExistente = await _repository.FindAsync(id);
            if (itemExistente == null)
            {
                throw new Exception("Item não encontrado.");
            }

            if (itemExistente.Matricula )

            itemExistente.Ativo = false;

            await _repository.EditAsync(itemExistente);
        }
    }
}
