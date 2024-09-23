using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _123Vendas.Vendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IVendasService _vendasService;

        public VendasController(IVendasService vendasService)
        {
            _vendasService = vendasService;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<VendaDTO>> GetAll()
        {
            try
            {
                var vendas = _vendasService.BuscarTodos();
                return Ok(vendas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<ValuesController>/{id}
        [HttpGet("{id}")]
        public ActionResult<VendaDTO> Get(string id)
        {
            try
            {
                Guid vendaId;

                if (Guid.TryParse(id, out vendaId))
                {
                    var venda = _vendasService.BuscarPorId(vendaId);

                    if (venda != null)
                    {
                        return Ok(venda);
                    }

                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<VendasController>
        [HttpPost]
        public ActionResult Post([FromBody] VendaDTO venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vendasService.Adicionar(venda);
                    return Created();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // PUT api/<ValuesController>
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] VendaDTO venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vendasService.Atualizar(venda);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                Guid vendaId;

                if (Guid.TryParse(id, out vendaId))
                {
                    var venda = _vendasService.Deletar(vendaId);

                    if (venda == null)
                    {
                        return NotFound();
                    }

                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
