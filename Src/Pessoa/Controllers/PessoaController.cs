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
        private readonly SorteioDbContext _context;
        private readonly PessoaHelpers _pessoaHelpers;
        public PessoaController(SorteioDbContext context, PessoaHelpers pessoaHelpers)
        {
            _context = context;
            _pessoaHelpers = pessoaHelpers;

        }

        [HttpPost]
        [Route("v1/cadastrar")]
        public IActionResult GerarNumero(PessoaModel pessoa)
        {
            if (pessoa == null || string.IsNullOrWhiteSpace(pessoa.Nome) || string.IsNullOrWhiteSpace(pessoa.Email))
            {

                return BadRequest("Campos Obrigatórios não fornecidos!");
            }

            // Checa se Email enviado existe
            if (EmailExistente(pessoa.Email))
            {
                if (CpfValidoParaEmail(pessoa.Email, pessoa.Cpf))
                {
                    string numero = _pessoaHelpers.GerarNumeroAleatorio(pessoa.Email);
                    SalvarNumeroNoBanco(numero, pessoa);
                    return Ok(new { Usuario = pessoa.Email, Numero = numero });
                }

                return BadRequest("CPF inválido para o email fornecido.");
            }

            // Criar novo usuário, caso Email não exista
            CadastrarPessoa(pessoa);
            return StatusCode(201, pessoa);
        }

        private bool EmailExistente(string email)
        {
            return _context.Pessoas.Any(p => p.Email == email);
        }

        private bool CpfValidoParaEmail(string email, string cpf)
        {
            return _context.Pessoas.Any(p => p.Email == email && p.Cpf == cpf);
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
                // Console.WriteLine($"Erro ao salvar o número no Banco: {ex}");
                throw new Exception($"Ocorreu um Erro ao salvar o número no Banco: {ex.Message}");
            }

        }

        public void CadastrarPessoa(PessoaModel pessoa)
        {

            try
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();

                string numero = _pessoaHelpers.GerarNumeroAleatorio(pessoa.Email);
                SalvarNumeroNoBanco(numero, pessoa);

            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Erro ao salvar usuário no Banco: {ex.Message}");
                throw new Exception($"Ocorreu um Erro ao salvar o Usuário no Banco: {ex.Message}");
            }

        }
    }
}