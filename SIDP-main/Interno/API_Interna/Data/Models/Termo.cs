using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_Interna.Data.Models
{
    [Table("termo")]
    public class Termo
    {
        [Key]
        [StringLength(60)]
        public string TermoId { get; set; }
        [Required]
        [StringLength(500)]
        [Column("nome_projeto")]
        public string NomeProjeto { get; set; }
        [Required]
        [StringLength(500)]
        [Column("objeto")]
        public string Objeto { get; set; }
        [Column("valor_total")]
        public decimal? ValorTotal { get; set; }
        [Required]
        [Column("qtd_beneficiarios")]
        public short QtdBeneficiarios { get; set; }
        [Required]
        [Column("tipo_contrato")]
        [StringLength(30)]
        public string TipoContrato { get; set; }
        [Column("sei_pagamento")]
        public int? SeiPagamento { get; set; }
        [Column("data_inicio")]
        public DateOnly? DataInicio { get; set; }
        [Column("data_termino")]
        public DateOnly? DataTermino { get; set; }
        [Column("data_assinatura")]
        public DateOnly? DataAssinatura { get; set; }
        [Column("origem_recurso")]
        public bool? IsEmenda { get; set; }
        [Column("edital")]
        public Edital? Edital { get; set; }
        [Required]
        [Column("osc_cnpj")]
        public OSC OSC { get; set; }
        [Column("representante_legal_cpf")]
        public RepresentanteLegal RepresentanteLegal { get; set; }


    }
    [Table("localizacao_termo")]
    public class LocalizacaoTermo
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
        public Termo Termo { get; set; }
    }
    [Table("aditivo_apostilamento")]
    public class AditivoApostilamento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("sei")]
        public int SeiAlteracao { get; set; }
        [Required]
        [Column("objeto")]
        public string Objeto { get; set; }
        [Required]
        [Column("tipo")]
        public bool IsAditivo { get; set; }
        [Required]
        [Column("termo")]
        public Termo Termo {get ; set; }
        
    }
    public class Alteracao
    {
        //todo: Talvez seja necessário um campo de tipo do atributo
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [Column("atributo")]
        public string Atributo { get; set; }
        [Required]
        [Column("valor_antigo")]
        [StringLength(500)]
        public string ValorAntigo { get; set; }
        [Required]
        public AditivoApostilamento AditivoApostilamento { get; set; }
    }
}
