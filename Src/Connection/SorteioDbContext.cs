using Microsoft.EntityFrameworkCore;
using Src.Pessoa.Models;

namespace Src.Connection
{

    public class SorteioDbContext : DbContext
    {
        public SorteioDbContext(DbContextOptions<SorteioDbContext> options) : base(options)
        {
        }
        public DbSet<PessoaModel> Pessoas { get; set; }
        public DbSet<NumeroModel> Numeros { get; set; }


    }
}