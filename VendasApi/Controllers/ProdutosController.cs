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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosService _produtosService;
        private readonly IRabbitMQPublisher<ProdutoDTO> _produtoMqPublisher;

        public ProdutosController(IProdutosService produtosService, IRabbitMQPublisher<ProdutoDTO> produtoMqPublisher)
        {
            _produtosService = produtosService;
            _produtoMqPublisher = produtoMqPublisher;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<List<VendaDTO>> BuscarTodos()
        {
            try
            {
                var vendas = _produtosService.BuscarTodos();
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
                    var venda = _produtosService.BuscarPorId(vendaId);

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

        // DELETE api/<ValuesController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Cancelar(string id)
        {
            try
            {
                Guid vendaId;

                if (Guid.TryParse(id, out vendaId))
                {
                    var vendaCancelada = _produtosService.Cancelar(vendaId);

                    if (vendaCancelada == null)
                    {
                        return NotFound();
                    }

                    await _produtoMqPublisher.PublicarMensagemAsync(vendaCancelada, RabbitMQFilas.VendasQueue);
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

