using EPBD2.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPBD2.DTOs
{
    public class NewOscDTO
    {
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public ICollection<RepresentanteDTO> RepresentantesLegais { get; set; }
        public ICollection<ContatoOscDto> ContatosOsc { get; set; }
        public ICollection<LocalizacaoOscDto> LocalizacoesOsc { get; set; }
        //public ICollection<Termo> Termos { get; set; }
        public NewOscDTO()
        {

        }

        public NewOscDTO(Osc o){
            Cnpj = o.Cnpj;
            Nome = o.Nome;
            LocalizacoesOsc = ConvertListDtoLoc(o.LocalizacoesOsc);
            RepresentantesLegais = ConvertListDtoRep(o.RepresentantesLegais);
            ContatosOsc = ConvertListDtoCont(o.ContatosOsc);
        }

        public Osc ConvertToEntity()
        {
            return new Osc
            {
                Cnpj = Cnpj,
                Nome = Nome,
                RepresentantesLegais = ConvertListRep(),
                ContatosOsc = ConvertListContatos(),
                LocalizacoesOsc = ConvertListLocs(),
            };
        }
        private ICollection<RepresentanteLegal> ConvertListRep()
        {
            var list = new List<RepresentanteLegal>();

            foreach (var rep in RepresentantesLegais) {
                var x = new RepresentanteLegal
                {
                    Cpf = rep.Cpf,
                    Email = rep.Email,
                    Nome = rep.Nome,
                    Telefone = rep.Telefone,
                    Termos = new List<Termo>()
                    
                };

                list.Add(x);
            }

            return list;
        }

        private ICollection<ContatoOsc> ConvertListContatos()
        {
            var list = new List<ContatoOsc>();

            foreach (var c in ContatosOsc)
            {
                var x = new ContatoOsc
                {
                    Contato = c.Contato,
                    Id = c.Id,
                    Tipo = c.Tipo,
                };

                list.Add(x);
            }

            return list;
        }

        private ICollection<LocalizacaoOsc> ConvertListLocs()
        {
            var list = new List<LocalizacaoOsc>();

            foreach (var c in LocalizacoesOsc)
            {
                var x = new LocalizacaoOsc
                {
                    Id = c.Id,
                    Distrito = c.Distrito,
                    Complemento = c.Complemento,
                    Cep = c.Cep,
                    Logradouro = c.Logradouro,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Numero = c.Numero,
                    Regiao = c.Regiao,
                    Subprefeitura = c.Subprefeitura,
                };

                list.Add(x);
            }

            return list;
        }

        private ICollection<RepresentanteDTO> ConvertListDtoRep(ICollection<RepresentanteLegal> list)
        {
            var res = new List<RepresentanteDTO>();

            foreach (var item in list)
            {
                RepresentanteDTO dto = item;
                res.Add(dto);
            }

            return res;
        }
        private ICollection<LocalizacaoOscDto> ConvertListDtoLoc(ICollection<LocalizacaoOsc> list)
        {
            var res = new List<LocalizacaoOscDto>();

            foreach (var item in list)
            {
                LocalizacaoOscDto dto = item;
                res.Add(dto);
            }

            return res;
        }
        private ICollection<ContatoOscDto> ConvertListDtoCont(ICollection<ContatoOsc> list)
        {
            var res = new List<ContatoOscDto>();

            foreach (var item in list)
            {
                ContatoOscDto dto = item;
                res.Add(dto);
            }

            return res;
        }
    }

    public class RepresentanteDTO
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public RepresentanteDTO()
        {
            
        }

        public RepresentanteDTO(RepresentanteLegal r)
        {
            Cpf = r.Cpf;
            Nome = r.Nome;
            Email = r.Email;
            Telefone = r.Telefone;
        }
        public static implicit operator RepresentanteDTO(RepresentanteLegal r) => new RepresentanteDTO(r);
    }

    public class ContatoOscDto
    {
        public int Id { get; set; }
        public short Tipo { get; set; }
        public string Contato { get; set; }

        public ContatoOscDto() { }
        public ContatoOscDto(ContatoOsc c)
        {
            Id = c.Id;
            Tipo = c.Tipo;
            Contato = c.Contato;
        }
        public static implicit operator ContatoOscDto(ContatoOsc c) => new ContatoOscDto(c);
    }

    public class LocalizacaoOscDto
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public short Distrito { get; set; }
        public short Subprefeitura { get; set; }
        public string Regiao { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public LocalizacaoOscDto() { }
        public LocalizacaoOscDto(LocalizacaoOsc l)
        {
            Id = l.Id;
            Logradouro = l.Logradouro;
            Numero = l.Numero;
            Complemento = l.Complemento;
            Cep = l.Cep;
            Distrito = l.Distrito;
            Subprefeitura = l.Subprefeitura;
            Regiao = l.Regiao;
            Latitude = l.Latitude;
            Longitude = l.Longitude;  
        }
        public static implicit operator LocalizacaoOscDto (LocalizacaoOsc l) => new LocalizacaoOscDto (l);
    }
}
