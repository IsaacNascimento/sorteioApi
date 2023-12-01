
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Src.Pessoa.Models
{
    [Table("Numero")]
    public class NumeroModel
    {
        [Key]
        [Required]
        [StringLength(5)]
        public required string Valor { get; set; }

        [Required]
        [StringLength(40)]
        [ForeignKey("Pessoa")]
        public string PessoaEmail { get; set; } = string.Empty;

        public PessoaModel? Pessoa { get; set; }

    }
}
