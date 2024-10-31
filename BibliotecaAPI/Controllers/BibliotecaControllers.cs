using BibliotecaAPI.Models;
using BibliotecaAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaControllers : ControllerBase
    {
        private readonly BibliotecaRepository _bibliotecaRepository;

        public BibliotecaControllers(BibliotecaRepository bibliotecaRepository)
        {
            _bibliotecaRepository = bibliotecaRepository;
        }
       

        // GET api/<BlibiotecaControllers>/5
        [HttpGet("Livros-cadastro")]
        public async Task<IActionResult> ListarLivros()
        {
            var livros = await _bibliotecaRepository.ListarLivros();
            return Ok(livros);
        }

        // POST api/<BlibiotecaControllers>
        [HttpPost("registrar-livro")]
        public async Task<IActionResult> CadastrarLivro ([FromBody] Livros livros)
        {
            return Ok(new { mensagem = "Livro Cadastrado com Sucesso!" });
        }

        // POST api/<BlibiotecaControllers>
        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuarios usuarios)
        {
            return Ok(new { mensagem = "Usuario Cadastrado com Sucesso!" });
        }

        // PUT api/<BlibiotecaControllers>/5
        [HttpPut("Tarifa-valor")]
        public async Task<IActionResult> Atualizar([FromBody] Livros livros)
        {
            await _bibliotecaRepository.Atualizar(livros);
            return Ok(new { memsagem = "Tarifa Atualizada com Sucesso" });
        }


        // DELETE api/<BlibiotecaControllers>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover uma Pessoa filtrando pelo ID.",
            Description = "Este endpoint é responsavel por remover os dados de uma pessoa no banco")]
        public async Task<IActionResult> Delete(int id)
        {
            Livros livros = await _bibliotecaRepository.BuscarPorId(id);

            if (livros == null)
            {
                return NotFound("Não existe pessoa cadastrada para o id informado");
            }

            await _bibliotecaRepository.DeletarPorId(id);
            return Ok();
        }

         // POST api/<BlibiotecaControllers>
        [HttpPost("registrar-emprestimo")]
        public async Task<IActionResult> CadastrarEmprestimo([FromBody] Emprestimo emprestimo)
        {
            return Ok(new { mensagem = "Emprestimo Cadastrado!" });
        }

       
        // POST api/<BlibiotecaControllers>
        [HttpPost("registrar-devolucao")]
        public async Task<IActionResult> CadastrarDevolucao([FromBody] Devolucoes devolucoes)
        {
            return Ok(new { mensagem = "Devouvimento Cadastrado!"});
        }

        // GET api/<BlibiotecaControllers>/5
        [HttpGet("Livros-emprestimo")]
        public async Task<IActionResult> ListarEmprestimo()
        {
            var emprestimos = await _bibliotecaRepository.ListarEmprestimo();
            return Ok(emprestimos);

        }

        [HttpGet("status-controle")]
        public async Task<IActionResult> ConsutarControleStatus()
        {
            var LivrosPadaoVagas = 100;
            var LivrosOcupadas = await _bibliotecaRepository.ListarLivrosOcupadasDB();
            var LivrosDisponiveis = LivrosPadaoVagas - LivrosOcupadas;

            return Ok(new { LivrosOcupadas, LivrosDisponiveis });
        }

        // GET api/<BlibiotecaControllers>/5
        [HttpGet("lista-Generos")]
        public async Task<IActionResult> BuscarPor()
        {
            var livros = await _bibliotecaRepository.BuscarPor();
            return Ok(livros);
        }

        // GET api/<BlibiotecaControllers>/5
        [HttpGet("buscar-usuario")]
        public async Task<IActionResult> BuscarUsuarioPor()
        {
            var usuarios = await _bibliotecaRepository.BuscarUsuarioPor();
            return Ok(usuarios);
        }

        //// POST api/<VeiculosController>
        //[HttpPost("registrar-saida-veiculo")]
        //public async Task<IActionResult> Regitrataxa([FromBody] Emprestimo emprestimo)
        //{
        //    if(int dia = Dataempestimo){
        //        int dia = DataEmprestimo +  0000 - 00 - 14 00:00:00;
        //            }
        //    //valor taxa
        //    decimal tarifaAtual = await _bibliotecaRepository.ValorTarifaDB();

        //    //data saida
        //    emprestimo.DataEmprestimo = DateTime.Now;

        //    //calculando horas
        //    var dias = (emprestimo.DataEmprestimo.Taxa - emprestimo.DataDevolucao).TotalDays;

        //    //valor pago
        //    emprestimo.Taxa = (decimal)dias + tarifaAtual;

        //    var veiculoId = await _bibliotecaRepository.RegistrarEntradaDB(Biblioteca);
        //    return Ok(new { mensagem = "Veiculo Cadastrado com Sucesso!" });
        //}

    }
}