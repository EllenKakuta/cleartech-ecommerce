using Ellen_Falpus_CadCategoria.Data.Dtos.CarrinhoDeComprasDto;
using Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ellen_Falpus_CadCategoria.Services
{
    public class BuscaCEPService
    {
        public async Task<CEPDto> BuscaCep(string cep)
        {
            HttpClient client = new HttpClient();
            var consulta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var resultado = await consulta.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<CEPDto>(resultado);
            return endereco;

        }


    }
}
