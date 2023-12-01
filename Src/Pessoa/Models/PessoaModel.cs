
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Src.Pessoa.Models
{
    [Table("Pessoa")]
    public class PessoaModel
    {
        [Key]
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [StringLength(40, ErrorMessage = "O campo Email deve ter no máximo 40 caracteres.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public required string Email { get; set; }

        [Required]
        [StringLength(30)]
        public required string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public required string Cpf { get; set; }

        [StringLength(15)]
        public string? Telefone { get; set; }
    }
}
