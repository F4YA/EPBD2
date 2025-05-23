namespace API_Interna.DTOs
{
    public class OSCDTO
    {
        public string cnpj {  get; set; }
        public string Nome { get; set; }
        public List<ContatoOscDTO> Contatos { get; set; }
        public List<RepresentanteLegalDTO> RepresentanteLegal { get; set; }
        public List<LocalizacaoDTO> Localizacoes { get; set; }

        public OSCDTO()
        {
            Contatos = new List<ContatoOscDTO>();
            RepresentanteLegal = new List<RepresentanteLegalDTO>();
            Localizacoes = new List<LocalizacaoDTO>();
        }

    }

    public class ContatoOscDTO
    {
        public int Id { get; set; }
        public byte Tipo { get; set; }
        public string Contato { get; set; }
    }

    public class RepresentanteLegalDTO
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
    }
    public class LocalizacaoDTO
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        public string Cep { get; set; }
        public byte Distrito { get; set; }
        public byte SubPrefeitura { get; set; }
        public string Regiao { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
