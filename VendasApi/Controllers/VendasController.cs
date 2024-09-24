using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Application.Interfaces.Publishers;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Infrastructure.RabbitMQ.Settings;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _123Vendas.Vendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IVendasService _vendasService;
        private readonly IRabbitMQPublisher<VendaDTO> _vendaMqPublisher;

        public VendasController(IVendasService vendasService, IRabbitMQPublisher<VendaDTO> vendaMqPublisher)
        {
            _vendasService = vendasService;
            _vendaMqPublisher = vendaMqPublisher;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<VendaDTO>> BuscarTodos()
        {
            try
            {
                List<VendaDTO> vendas = _vendasService.BuscarTodos();
                return Ok(vendas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<ValuesController>/{id}
        [HttpGet("{id}")]
        public ActionResult<VendaDTO> BuscarPorId(string id)
        {
            try
            {
                Guid vendaId;

                if (Guid.TryParse(id, out vendaId))
                {
                    VendaDTO venda = _vendasService.BuscarPorId(vendaId);

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
        public async Task<ActionResult<VendaDTO>> Adicionar([FromBody] VendaDTO venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VendaDTO novaVenda = _vendasService.Adicionar(venda);
                    await _vendaMqPublisher.PublicarMensagemAsync(novaVenda, RabbitMQFilas.VendasQueue);
                    return Ok(novaVenda);
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
        public async Task<ActionResult<VendaDTO>> Atualizar([FromBody] VendaDTO venda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VendaDTO vendaAtualizada = _vendasService.Atualizar(venda);
                    await _vendaMqPublisher.PublicarMensagemAsync(vendaAtualizada, RabbitMQFilas.VendasQueue);
                    return Ok(vendaAtualizada);
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
        public async Task<ActionResult<VendaDTO>> Cancelar(string id)
        {
            try
            {
                Guid vendaId;

                if (Guid.TryParse(id, out vendaId))
                {
                    VendaDTO vendaCancelada = _vendasService.Cancelar(vendaId);

                    if (vendaCancelada == null)
                    {
                        return NotFound();
                    }

                    await _vendaMqPublisher.PublicarMensagemAsync(vendaCancelada, RabbitMQFilas.VendasQueue);
                    return Ok(vendaCancelada);
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
