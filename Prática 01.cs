using Microsoft.AspNetCore.Mvc;

namespace Pessoas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        public class Pessoa
        {
            public string Nome { get; set; }
            public double Peso { get; set; }
            public double Altura { get; set; }
        }

        public class ResultadoImc
        {
            public string Nome { get; set; }
            public double Imc { get; set; }
            public string Descricao { get; set; }
        }

        [HttpPost("calcular-imc")]
        public IActionResult CalcularImc([FromBody] Pessoa pessoa)
        {
            if (pessoa == null || pessoa.Peso <= 0 || pessoa.Altura <= 0)
            {
                return BadRequest("Dados inválidos. Verifique se o peso e a altura são maiores que zero.");
            }

            double imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
            
            return Ok(new
            {
                Nome = pessoa.Nome,
                Imc = imc
            });
        }

        [HttpGet("consulta-tabela-imc")]
        public IActionResult ConsultaTabelaImc([FromQuery] double imc)
        {
            string descricao;

            if (imc < 18.5)
            {
                descricao = "Abaixo do peso";
            }
            else if (imc >= 18.5 && imc <= 24.9)
            {
                descricao = "Peso normal";
            }
            else if (imc >= 25.0 && imc <= 29.9)
            {
                descricao = "Sobrepeso";
            }
            else if (imc >= 30.0 && imc <= 34.9)
            {
                descricao = "Obesidade grau 1";
            }
            else if (imc >= 35.0 && imc <= 39.9)
            {
                descricao = "Obesidade
