using Microsoft.AspNetCore.Mvc;
using PerdiNoCampus.API.Contracts;
using PerdiNoCampus.API.Models;
using PerdiNoCampus.API.Services.Interfaces;

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

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAsync([FromBody] CreateItemRequest request)
        {
            var requestToItem = new ItemModel
            {
                Nome = request.Nome,
                CategoriaItem = request.CategoriaItem,
                LocalEncontrado = request.LocalEncontrado,
                TurnoEncontrado = request.TurnoEncontrado,
                UsarioNomeLocalizou = request.UsarioNomeLocalizou,
                ImagemUrl = request.ImagemUrl,
            };

            await _itemService.CriarAsync(requestToItem);

            return Created(nameof(PostAsync), new { id = requestToItem.Id });
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ItemResponse>>> GetAsync()
        {
            var items = await _itemService.ObterTodosAsync();
            var entitiesToDto = items.Select(item => new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                CategoriaItem = item.CategoriaItem,
                LocalEncontrado = item.LocalEncontrado,
                TurnoEncontrado = item.TurnoEncontrado,
                UsarioNomeLocalizou = item.UsarioNomeLocalizou,
                ImagemUrl = item.ImagemUrl,
                FoiRecuperado = item.FoiRecuperado,
                FoiEntregueAPrefeitura = item.FoiEntregueAPrefeitura,
                CriadoEm = item.CriadoEm
            }).ToList();

            return Ok(entitiesToDto);
        }

        [HttpGet("nome")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ItemResponse>>> GetByNameAsync([FromQuery] string nome)
        {
            var items = await _itemService.ObterTodosAsync(x => x.Nome.Contains(nome));
            var entitiesToDto = items.Select(item => new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                CategoriaItem = item.CategoriaItem,
                LocalEncontrado = item.LocalEncontrado,
                TurnoEncontrado = item.TurnoEncontrado,
                UsarioNomeLocalizou = item.UsarioNomeLocalizou,
                ImagemUrl = item.ImagemUrl,
                CriadoEm = item.CriadoEm
            }).ToList();

            return Ok(entitiesToDto);
        }

        [HttpGet("categoria")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ItemResponse>>> GetByCategoryAsync([FromQuery] string categoria)
        {
            var items = await _itemService.ObterTodosAsync(x => x.CategoriaItem.ToString().Contains(categoria));
            var entitiesToDto = items.Select(item => new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                CategoriaItem = item.CategoriaItem,
                LocalEncontrado = item.LocalEncontrado,
                TurnoEncontrado = item.TurnoEncontrado,
                UsarioNomeLocalizou = item.UsarioNomeLocalizou,
                ImagemUrl = item.ImagemUrl,
                CriadoEm = item.CriadoEm
            }).ToList();

            return Ok(entitiesToDto);
        }

        [HttpGet("encontrados")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<ItemResponse>>> GetFoundItemsAsync()
        {
            var items = await _itemService.ObterTodosAsync(x => x.FoiRecuperado == true);
            var entitiesToDto = items.Select(item => new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                CategoriaItem = item.CategoriaItem,
                LocalEncontrado = item.LocalEncontrado,
                TurnoEncontrado = item.TurnoEncontrado,
                UsarioNomeLocalizou = item.UsarioNomeLocalizou,
                ImagemUrl = item.ImagemUrl,
                CriadoEm = item.CriadoEm
            }).ToList();

            return Ok(entitiesToDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ItemResponse>> GetByIdAsync([FromRoute] int id)
        {
            var item = await _itemService.ObterPorIdAsync(id);
            var itemToDto = new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                CategoriaItem = item.CategoriaItem,
                LocalEncontrado = item.LocalEncontrado,
                TurnoEncontrado = item.TurnoEncontrado,
                UsarioNomeLocalizou = item.UsarioNomeLocalizou,
                ImagemUrl = item.ImagemUrl,
                CriadoEm = item.CriadoEm
            };

            return Ok(itemToDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateItemRequest request)
        {
            var requestToItem = new ItemModel
            {
                Id = id,
                Nome = request.Nome,
                CategoriaItem = request.CategoriaItem,
                LocalEncontrado = request.LocalEncontrado,
                TurnoEncontrado = request.TurnoEncontrado,
                ImagemUrl = request.ImagemUrl,
                FoiEntregueAPrefeitura = request.FoiEntregueAPrefeitura,
                FoiRecuperado = request.FoiRecuperado
            };

            await _itemService.AtualizarAsync(requestToItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _itemService.DeletarAsync(id);
            return NoContent();
        }
    }
}
