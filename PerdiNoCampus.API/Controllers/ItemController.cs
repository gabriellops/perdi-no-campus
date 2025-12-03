using Microsoft.AspNetCore.Mvc;
using PerdiNoCampus.API.Contracts;
using PerdiNoCampus.API.Models;
using PerdiNoCampus.API.Services.Interfaces;
using System.Xml.Linq;

namespace PerdiNoCampus.API.Controllers
{
    [ApiController]
    [Route("items")]
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
                Matricula = request.Matricula ?? 0,
                ImagemUrl = request.ImagemUrl,
                FoiEntregueAPrefeitura = request.FoiEntregueAPrefeitura ?? false
            };

            await _itemService.CriarAsync(requestToItem);

            return Created(nameof(PostAsync), new { id = requestToItem.Id });
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ItemResponse>>> GetAsync()
        {
            var items = await _itemService.ObterTodosAsync(x => x.Ativo && x.FoiRecuperado == false);

            var response = items.Select(x => new ItemResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                CategoriaItem = x.CategoriaItem,
                LocalEncontrado = x.LocalEncontrado,
                TurnoEncontrado = x.TurnoEncontrado,
                UsarioNomeLocalizou = x.UsarioNomeLocalizou,
                Matricula = x.Matricula,
                ImagemUrl = x.ImagemUrl,
                FoiRecuperado = x.FoiRecuperado,
                FoiEntregueAPrefeitura = x.FoiEntregueAPrefeitura,
                Ativo = x.Ativo,
                CriadoEm = x.CriadoEm
            }).ToList();

            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ItemResponse>>> GetByNameAsync([FromQuery] string nome)
        {
            nome = nome?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nome))
            {
                // se vier vazio, devolve o mesmo que o GetAll
                var all = await _itemService.ObterTodosAsync(x => x.Ativo && x.FoiRecuperado == false);
                return Ok(all);
            }

            var items = await _itemService.ObterTodosAsync(x =>
                                            x.Ativo &&
                                            x.FoiRecuperado == false &&
                                            (
                                                x.Nome.ToLower().Contains(nome.ToLower()) ||
                                                x.Descricao.ToLower().Contains(nome.ToLower()) ||
                                                x.LocalEncontrado.ToLower().Contains(nome.ToLower())
                                            ));

            var response = items.Select(x => new ItemResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                CategoriaItem = x.CategoriaItem,
                LocalEncontrado = x.LocalEncontrado,
                TurnoEncontrado = x.TurnoEncontrado,
                UsarioNomeLocalizou = x.UsarioNomeLocalizou,
                Matricula = x.Matricula,
                ImagemUrl = x.ImagemUrl,
                FoiRecuperado = x.FoiRecuperado,
                FoiEntregueAPrefeitura = x.FoiEntregueAPrefeitura,
                Ativo = x.Ativo,
                CriadoEm = x.CriadoEm
            }).ToList();

            return Ok(response);
        }

        [HttpGet("categoria")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ItemResponse>>> GetByCategoryAsync([FromQuery] string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                return BadRequest("Categoria é obrigatória.");

            categoria = categoria.Trim();

            var items = await _itemService.ObterTodosAsync(x =>
                x.Ativo &&
                x.FoiRecuperado == false &&
                x.CategoriaItem.ToString() == categoria
            );

            var response = items.Select(x => new ItemResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                CategoriaItem = x.CategoriaItem,
                LocalEncontrado = x.LocalEncontrado,
                TurnoEncontrado = x.TurnoEncontrado,
                UsarioNomeLocalizou = x.UsarioNomeLocalizou,
                Matricula = x.Matricula,
                ImagemUrl = x.ImagemUrl,
                FoiRecuperado = x.FoiRecuperado,
                FoiEntregueAPrefeitura = x.FoiEntregueAPrefeitura,
                Ativo = x.Ativo,
                CriadoEm = x.CriadoEm
            }).ToList();

            return Ok(response);
        }

        [HttpGet("resgatados")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ItemResponse>>> GetFoundItemsAsync()
        {
            var items = await _itemService.ObterTodosAsync(x =>
                x.FoiRecuperado == true
             );

            var response = items.Select(x => new ItemResponse
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao,
                CategoriaItem = x.CategoriaItem,
                LocalEncontrado = x.LocalEncontrado,
                TurnoEncontrado = x.TurnoEncontrado,
                UsarioNomeLocalizou = x.UsarioNomeLocalizou,
                Matricula = x.Matricula,
                ImagemUrl = x.ImagemUrl,
                FoiRecuperado = x.FoiRecuperado,
                FoiEntregueAPrefeitura = x.FoiEntregueAPrefeitura,
                Ativo = x.Ativo,
                CriadoEm = x.CriadoEm
            }).ToList();

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] UpdateItemRequest request)
        {
            if (id != request.Id)
                return BadRequest("Id da rota diferente do corpo da requisição.");

            var requestToItem = new ItemModel
            {
                Id = id,
                Nome = request.Nome,
                Descricao = request.Descricao,
                CategoriaItem = request.CategoriaItem,
                LocalEncontrado = request.LocalEncontrado,
                TurnoEncontrado = request.TurnoEncontrado,
                UsarioNomeLocalizou = request.UsarioNomeLocalizou,
                ImagemUrl = request.ImagemUrl,
                FoiEntregueAPrefeitura = request.FoiEntregueAPrefeitura,
                FoiRecuperado = request.FoiRecuperado
            };

            await _itemService.AtualizarAsync(requestToItem);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _itemService.DeletarAsync(id);
            return NoContent();
        }
    }
}
