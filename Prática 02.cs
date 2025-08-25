namespace CalcularFreteAPI.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public float PesoKg { get; set; }
        public float AlturaCm { get; set; }
        public float LarguraCm { get; set; }
        public float ComprimentoCm { get; set; }
        public string UF { get; set; }
    }
}
using CalcularFrete Models;

namespace CalcularFreteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FreteController : ControllerBase
    {
        private static readonly Dictionary<string, float> TaxasPorEstado = new Dictionary<string, float>
        {
            { "SP", 50.00f },
            { "RJ", 60.00f },
            { "MG", 55.00f },
            { "OUTROS", 70.00f }
        };

        private const float TaxaPorCm3 = 0.01f;

        [HttpPost("calcularfrete")]
        public ActionResult<object> CalcularFrete([FromBody] Produto produto)
        {
            if (produto == null || string.IsNullOrEmpty(produto.UF))
            {
                return BadRequest("Dados do produto ou UF são inválidos.");
            }

            float volume = produto.AlturaCm * produto.LarguraCm * produto.ComprimentoCm;
            float valorFreteVolume = volume * TaxaPorCm3;

            string ufNormalizada = produto.UF.ToUpper();
            float taxaEstado = TaxasPorEstado.GetValueOrDefault(ufNormalizada, TaxasPorEstado["OUTROS"]);

            float valorTotalFrete = valorFreteVolume + taxaEstado;

            return Ok(new { ValorFrete = Math.Round(valorTotalFrete, 2) });
        }
    }
}


