using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculoJurosController : Controller
    {
        private readonly ITaxaJurosService _taxaJurosService;
        private readonly ILogger<CalculoJurosController> _logger;
        public CalculoJurosController(ITaxaJurosService taxaJurosService, 
            ILogger<CalculoJurosController> logger)
        {
            _taxaJurosService = taxaJurosService;
            _logger = logger;
        }

        /// <summary>
        /// Método para calcular o Montante de um cálculo de Juros Compostos
        /// </summary>
        /// <param name="valorInicial">100</param>
        /// <param name="tempo">5</param>
        /// <returns>Montante truncado em 2 casas decimais</returns>
        [HttpGet]
        [Route("calculaJuros")]
        public async Task<IActionResult> CalcularJurosAsync([FromQuery] decimal valorInicial, [FromQuery] int tempo)
        {
            _logger.LogInformation($"Info:Acesso ao endpoint de Calculo de Juros");
            try
            {
                var montante = await _taxaJurosService.CalcularMontanteAsync(valorInicial, tempo);
                return Ok(montante);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro depois de tentar acesso à API de Taxa");
                throw;
            }
           
        }

        /// <summary>
        /// Mostra o caminho para os repositórios do GitHub
        /// </summary>
        /// <returns>URL para repositórios</returns>
        [HttpGet]
        [Route("showmethecode")]
        public List<Uri> ShowMeTheCode()
        {
            var repositorios = new List<Uri>();
            repositorios.Add(new Uri("https://github.com/navarro-hubess/ECommerce"));
            repositorios.Add(new Uri("https://github.com/navarro-hubess/ECommerce.Taxas"));
            return repositorios;
        }
    }
}
