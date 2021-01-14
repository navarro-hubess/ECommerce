using Config;
using Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service
{

    public interface ITaxaJurosService
    {
        Task<decimal> CalcularMontanteAsync(decimal valorInicial, int tempo);
        Task<decimal> BuscarTaxa();
    }

    public class TaxaJurosService : ITaxaJurosService
    {

        private readonly HttpClient _httpClient;
        private readonly ITaxaJurosApiConfig _taxaJurosApiConfig;

        public TaxaJurosService(HttpClient httpClient, ITaxaJurosApiConfig taxaJurosApiConfig)
        {

            _httpClient = httpClient;
            _taxaJurosApiConfig = taxaJurosApiConfig;
        }

        public async Task<decimal> BuscarTaxa()
        {
            var response = await _httpClient.GetAsync($"{_taxaJurosApiConfig.BaseUrl}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            var teste = JsonConvert.DeserializeObject<TaxaJuros>(apiResponse).TaxaDeJuros;
            return teste;
        }

        public async Task<decimal> CalcularMontanteAsync(decimal valorInicial, int tempo)
        {
            decimal juros = await BuscarTaxa();
            Financial financial = new Financial();
            return financial.CalculaJuros(valorInicial, tempo, juros); ;

        }


    }
}

