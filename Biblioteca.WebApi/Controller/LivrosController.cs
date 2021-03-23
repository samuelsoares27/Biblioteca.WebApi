using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models.Models;
using Biblioteca.WebApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/livro")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly IRepository<Livros> _repository;
        private readonly IMapper _mapper;

        public LivrosController(IRepository<Livros> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LivroDto>> Listar()
        {
            var resposta = await _repository.GetAll();

            return _mapper.Map<IEnumerable<LivroDto>>(resposta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDto>> BuscarPorId(int id)
        {
            if (id.Equals(null))
                return BadRequest("Id não foi enviado para API");

            var resposta = await _repository.GetById(id);

            return _mapper.Map<LivroDto>(resposta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, LivroDto modelo)
        {
            if (modelo is null)
                return BadRequest("Modelo Nulo");

            if (id != modelo.LivroId)
                return BadRequest("Id diferentes");

            if (_repository.GetById(id) is null)
                return BadRequest("Id não encontrado");

            var resposta = _mapper.Map<Livros>(modelo);

            if (await _repository.Update(resposta) > 0)
                return CreatedAtAction($"api/livro/{resposta.LivroId}", modelo);

            return BadRequest("Erro na alteração");
        }

        [HttpPost]
        public async Task<ActionResult<LivroDto>> Inserir(LivroDto modelo)
        {
            if (modelo is null)
                BadRequest("Modelo nulo");

            var resposta = _mapper.Map<Livros>(modelo);

            if (await _repository.Add(resposta) > 0)
                return Ok();

            return BadRequest("Erro na Inclusão");

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Livros>> Remover(int id)
        {
            if (id.Equals(null))
                BadRequest("Id não foi enviado para API");

            var modelo = await _repository.GetById(id);
            if (modelo is null)
                BadRequest("Id não encontrado");

            if (await _repository.Delete(modelo) > 0)
                return CreatedAtAction("DeleteProduto", new { id = id });

            return BadRequest("Erro na exclusão");
        }

    }
}
