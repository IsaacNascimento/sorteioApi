using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Src.Connection;
using Src.Pessoa.Helpers;
using Src.Pessoa.Models;

namespace Src.Pessoa.Controllers
{
    [ActivatorUtilitiesConstructor]
    public class PessoaController : Controller
    {
        PessoaHelpers pessoaHelpers = new();
        private readonly SorteioDbContext _context;
        public PessoaController(SorteioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("v1/cadastrar")]
        public IActionResult GerarNumero(PessoaModel pessoa)
        {
            if (pessoa == null) return BadRequest("O Campos são Obrigatórios!");

            // Checa se Email enviado existe
            var existingEmail = _context.Pessoas.Any(p => p.Email == pessoa.Email);
            if (existingEmail)
            {
                // Se o email existe, verifica se o CPF pertence ao mesmo usuário
                var existingCpfForEmail = _context.Pessoas.Any(p => p.Email == pessoa.Email && p.Cpf == pessoa.Cpf);

                if (existingCpfForEmail)
                {

                    string numero = pessoaHelpers.GerarNumeroAleatorio(pessoa.Email);
                    SalvarNumeroNoBanco(numero, pessoa);
                    return Ok(new { Usuario = pessoa.Email, Numero = numero });
                }

                // Usuario não existente para Email e Cpf Fornecido;
                return BadRequest("CPF inválido para o email fornecido.");
            }

            // Criar novo usuário, caso Email não exista
            CadastrarPessoa(pessoa);
            return StatusCode(201, pessoa);
        }

        public void SalvarNumeroNoBanco(string numero, PessoaModel pessoa)
        {

            try
            {
                NumeroModel numeroModel = new NumeroModel
                {
                    Valor = numero,
                    PessoaEmail = pessoa.Email
                };

                _context.Numeros.Add(numeroModel);
                _context.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao salvar o número no Banco: {ex}");


                throw new Exception($"Ocorreu um Erro ao salvar o número no Banco: {ex.Message}");


            }

        }

        public void CadastrarPessoa(PessoaModel pessoa)
        {

            try
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();

                string numero = pessoaHelpers.GerarNumeroAleatorio(pessoa.Email);
                SalvarNumeroNoBanco(numero, pessoa);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar usuário no Banco: {ex.Message}");
                throw new Exception($"Ocorreu um Erro ao salvar o Usuário no Banco: {ex.Message}");
            }

        }
    }
}