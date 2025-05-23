using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Interna.Data.Models
{
    public class EmendaParlamentar
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("valor")]
        public decimal Valor { get; set; }
        [Required]
        [Column("termo")]
        public Termo Termo { get; set; }
        [Required]
        [Column("vereador")]
        public Vereador Vereador { get; set; }

    }

    public class Vereador
    {
        [Key]
        [StringLength(30)]
        public string Cpf { get; set; }
        [Required]
        [StringLength(60)]
        public string Nome { get; set; }
        [Required]
        [StringLength(200)]
        public string Contato { get; set; }
    }
}
