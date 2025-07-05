using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPBD2.Models
{
    public class Osc
    {
        [Key, Column("cnpj"),StringLength(14)]
        public string Cnpj { get; set; }

        [Required, Column("nome"), StringLength(120)]
        public string Nome { get; set; }
        public ICollection<RepresentanteLegal> RepresentantesLegais { get; set; }
        public ICollection<ContatoOsc> ContatosOsc { get; set; }
        public ICollection<LocalizacaoOsc> LocalizacoesOsc { get; set; }
        public ICollection<Termo> Termos { get; set; }

        public Osc()
        {
            RepresentantesLegais = new List<RepresentanteLegal>();
            ContatosOsc = new List<ContatoOsc>();
            LocalizacoesOsc = new List<LocalizacaoOsc>();
            Termos = new List<Termo>();
        }
    }

    public class RepresentanteLegal
    {
        [Key, Column("cpf"), StringLength(11)]
        public string Cpf { get; set; }

        [Required, Column("nome"), StringLength(60)]
        public string Nome { get; set; }

        [Required, Column("email"), StringLength(120), EmailAddress]
        public string Email { get; set; }

        [Required, Column("telefone"), StringLength(12)]
        public string Telefone { get; set; }

        [Column("osc_cnpj")]
        public Osc? Osc { get; set; }
        public ICollection<Termo> Termos { get; set; }
        public RepresentanteLegal()
        {
            Termos = new List<Termo>();
        }
    }

    public class ContatoOsc
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("tipo")]
        public short Tipo { get; set; }

        [Required, Column("contato"), StringLength(120)]
        public string Contato { get; set; }
        [Column("osc_cnpj")]
        public Osc? Osc { get; set; }

        public ContatoOsc() { }
    }

    public class LocalizacaoOsc
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("logradouro"), StringLength(180)]
        public string Logradouro { get; set; }

        [Required, Column("numero"), StringLength(5)]
        public string Numero { get; set; }

        [Column("complemento"), StringLength(30)]
        public string Complemento { get; set; }

        [Required, Column("cep"), StringLength(8)]
        public string Cep { get; set; }

        [Required, Column("distrito")]
        public short Distrito { get; set; }

        [Required, Column("subprefeitura")]
        public short Subprefeitura { get; set; }

        [Required, Column("regiao"), StringLength(1)]
        public string Regiao { get; set; }

        [Column("latitude")]
        public decimal? Latitude { get; set; }

        [Column("longitude")]
        public decimal? Longitude { get; set; }

        [Column("osc_cnpj")]
        public Osc? Osc { get; set; }

        public LocalizacaoOsc() { }
    }

    public class Edital
    {
        [Key, Column("nome"), StringLength(60)]
        public string Nome { get; set; }

        [Required, Column("objeto"), StringLength(500)]
        public string Objeto { get; set; }

        [Required, Column("data_publicacao")]
        public DateTime DataPublicacao { get; set; }

        [Column("data_resultado_parcial")]
        public DateTime? DataResultadoParcial { get; set; }

        [Required, Column("data_resultado_definitivo")]
        public DateTime DataResultadoDefinitivo { get; set; }

        [Required, Column("data_homologacao")]
        public DateTime DataHomologacao { get; set; }

        [Required, Column("num_lotes")]
        public int NumLotes { get; set; }

        [Required, Column("num_equipamentos")]
        public int NumEquipamentos { get; set; }

        [Required, Column("situacao"), StringLength(30)]
        public string Situacao { get; set; }

        public ICollection<Termo> Termos { get; set; }

        public Edital()
        {
            Termos = new List<Termo>();
        }
    }

    public class Vereador
    {
        [Key, Column("cpf"), StringLength(11)]
        public string Cpf { get; set; }

        [Required, Column("nome"), StringLength(60)]
        public string Nome { get; set; }

        [Required, Column("contato"), StringLength(200)]
        public string Contato { get; set; }
        public ICollection<EmendaParlamentar> EmendasParlamentares { get; set; }
        public Vereador()
        {
            EmendasParlamentares = new List<EmendaParlamentar>();
        }
    }

    public class Termo
    {
        [Key, Column("termo"), StringLength(60)]
        public string TermoNome { get; set; }

        [Required, Column("nome_projeto"), StringLength(500)]
        public string NomeProjeto { get; set; }

        [Required, Column("objeto"), StringLength(500)]
        public string Objeto { get; set; }

        [Required, Column("qtd_beneficarios")]
        public short QtdBeneficarios { get; set; }

        [Required, Column("tipo_contrato"), StringLength(30)]
        public string TipoContrato { get; set; }

        [Column("valor_total")]
        public decimal? ValorTotal { get; set; }

        [Column("sei_pagamento")]
        public int? SeiPagamento { get; set; }

        [Column("data_inicio")]
        public DateTime? DataInicio { get; set; }

        [Column("data_termino")]
        public DateTime? DataTermino { get; set; }

        [Column("data_assinatura")]
        public DateTime? DataAssinatura { get; set; }

        [Column("origem_recurso")]
        public bool? OrigemRecurso { get; set; }

        [Column("edital"), StringLength(60)]
        public Edital? Edital { get; set; }

        [Column("osc_cnpj")]
        public Osc? Osc { get; set; }
        [Column("cpf_representante_legal")]
        public RepresentanteLegal? RepresentanteLegal { get; set; }

        public ICollection<EmendaParlamentar> EmendasParlamentares { get; set; }
        public ICollection<LocalizacaoTermo> LocalizacoesTermo { get; set; }
        public ICollection<AditivoApostilamento> AditivosApostilamentos { get; set; }

        public Termo()
        {
            EmendasParlamentares = new List<EmendaParlamentar>();
            LocalizacoesTermo = new List<LocalizacaoTermo>();
            AditivosApostilamentos = new List<AditivoApostilamento>();
        }
    }

    public class EmendaParlamentar
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("valor")]
        public decimal Valor { get; set; }

        [Column("cpf_vereador")]
        public Vereador? Vereador { get; set; }
        [Column("termo")]
        public Termo? Termo { get; set; }

        public EmendaParlamentar() { }
    }

    public class LocalizacaoTermo
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("logradouro"), StringLength(180)]
        public string Logradouro { get; set; }

        [Required, Column("numero"), StringLength(5)]
        public string Numero { get; set; }

        [Column("complemento"), StringLength(30)]
        public string Complemento { get; set; }

        [Required, Column("cep"), StringLength(8)]
        public string Cep { get; set; }

        [Required, Column("distrito")]
        public short Distrito { get; set; }

        [Required, Column("subprefeitura")]
        public short Subprefeitura { get; set; }

        [Required, Column("regiao"), StringLength(1)]
        public string Regiao { get; set; }

        [Column("latitude")]
        public decimal? Latitude { get; set; }

        [Column("longitude")]
        public decimal? Longitude { get; set; }

        [Column("termo")]
        public Termo? Termo { get; set; }

        public LocalizacaoTermo() { }
    }

    public class AditivoApostilamento
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("sei")]
        public int Sei { get; set; }

        [Required, Column("objeto"), StringLength(200)]
        public string Objeto { get; set; }

        [Required, Column("tipo")]
        public bool Tipo { get; set; }

        [Column("termo")]
        public Termo? Termo { get; set; }

        public ICollection<Alteracoes> Alteracoes { get; set; }

        public AditivoApostilamento()
        {
            Alteracoes = new List<Alteracoes>();
        }
    }

    public class Alteracoes
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("atributo"), StringLength(30)]
        public string Atributo { get; set; }

        [Required, Column("valor_antigo"), StringLength(500)]
        public string ValorAntigo { get; set; }

        [Column("id_adt_apt")]
        public AditivoApostilamento AditivoApostilamento { get; set; }

        public Alteracoes() { }
    }
}
