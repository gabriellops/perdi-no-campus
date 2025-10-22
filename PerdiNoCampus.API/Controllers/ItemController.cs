using Microsoft.AspNetCore.Mvc;
using PerdiNoCampus.API.Contracts;
using PerdiNoCampus.API.Services;

namespace PerdiNoCampus.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemResponse>>> GetAsync()
        {
            var items = await _itemService.ObterTodosItemsAsync();
            return Ok(items);
        }

        [HttpGet("nome")]
        public async Task<ActionResult<List<ItemResponse>>> GetByName(string nome)
        {
            var items = await _itemService.ObterTodosAsync(x => x.Nome.Contains(nome));
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemResponse>>> GetByCategory(string categoria)
        {
            var items = await _itemService.ObterTodosAsync(x => x.Categoria.Contains(categoria));
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse>> CreateAsync(CreateItemRequest request)
        {
            var item = await _itemService.CriarItemAsync(request);
            return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
        }
}
