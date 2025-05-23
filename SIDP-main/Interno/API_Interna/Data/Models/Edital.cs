using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Interna.Data.Models
{
    [Table("edital")]
    public class Edital
    {
        [Key]
        [StringLength(60, ErrorMessage = "O nome do edital não pode ultrapassar 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O objeto do edital não pode ser nulo")]
        [StringLength(500, ErrorMessage = "O objeto do edital não pode ultrapassar 500 caracteres")]
        public string Objeto { get; set; }

        [Required(ErrorMessage = "A data de publicação não pode ser nula")]
        public DateOnly DataPublicacao { get; set; }

        [Required(ErrorMessage = "A data do resultado parcial não pode ser nula")]
        public DateOnly DataResultadoParcial { get; set; }

        [Required(ErrorMessage = "A data do resultado definitivo não pode ser nula")]
        public DateOnly DataResultadoDefinitivo { get; set; }

        [Required(ErrorMessage = "A data de Homologação não pode ser nula")]
        public DateOnly DataHomologacao { get; set; }

        [Required(ErrorMessage = "A quantidade de lotes não pode ser nula")]
        public short NumLotes { get; set; }

        [Required(ErrorMessage = "A quantidade de equipamentos não pode ser nula")]
        public short NumEquipamentos { get; set; }

        [Required(ErrorMessage = "A situação do edital não pode ser nula")]
        public bool Situacao { get; set; }

    }
}
