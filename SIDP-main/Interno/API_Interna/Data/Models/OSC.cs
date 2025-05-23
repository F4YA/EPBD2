using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Interna.Data.Models
{
    [Table("osc")]
    public class OSC
    {
        [Key]
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "O nome da OSC não pode ser nulo")]
        [StringLength(120, ErrorMessage = "O nome da OSC não pode ultrapassar 120 Caracteres")]
        public string Nome { get; set; }
        [Required]
        public List<ContatoOSC> Contatos { get; set; }
        [Required]
        public List<RepresentanteLegal> RepresentantesLegais { get; set; }
        [Required]
        public List<LocalizacaoOSC> Localizacoes { get; set; }
        public List<Termo>? projetos { get; set; }

    }

    [Table("contato_osc")]
    public class ContatoOSC
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O tipo de contato não pode ser nulo")]
        public byte Tipo { get; set; }
        [Required(ErrorMessage = "O contato não pode ser nulo")]
        [StringLength(120, ErrorMessage = "O contato não pode ultrapassar 120 caracteres")]
        public string Contato { get; set; } 
        public OSC OSC { get; set; }
    }

    [Table("representante_legal")]
    public class RepresentanteLegal
    {
        [Key] 
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O nome do representante não pode ser nulo")]
        [StringLength(60, ErrorMessage = "O nome do representante não pode ultrapassar 60 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O email do representante não pode ser nulo")]
        [StringLength(120, ErrorMessage = "O email não pode ultrapassar 120 caracteres")]
        [EmailAddress(ErrorMessage = "O formato do email é invalido")]
        public string email { get; set; }
        public string telefone { get; set; }
        public OSC? OSC { get; set; }
    }

    [Table("localizacao_osc")]
    public class LocalizacaoOSC
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O logradouro não pode ser nulo")]
        [StringLength(180, ErrorMessage = "O logradouro não pode ultrapassar 180 caracteres")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O numero não pode ser nulo")]
        [StringLength(5, ErrorMessage = "O numero número não pode ultrapassar 5 caracteres")]
        public string Numero { get; set; }
        [StringLength(30, ErrorMessage = "O complemento não pode ultrapassar 30 caracteres")]
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "O CEP não pode ser nulo")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "O distrito não pode ser nulo")]
        public byte Distrito { get; set; }
        [Required(ErrorMessage = "A Subprefeitura não pode ser nula")]
        public byte SubPrefeitura { get; set; }
        [Required(ErrorMessage = "A Região não pode ser nula")]
        [StringLength(1)]
        public string Regiao { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public OSC OSC { get; set; }
    }
}
