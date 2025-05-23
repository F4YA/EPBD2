using API_Interna.Data.Models;
using API_Interna.DTOs;
using API_Interna.Interfaces.Business;
using API_Interna.Interfaces.Repositorio;

namespace API_Interna.Business
{
    public class OSCBusiness : IOSCBusiness
    {
        public readonly IOSCRepository _oscRepository;
        public OSCBusiness(IOSCRepository OscRepository)
        {
            _oscRepository = OscRepository;
        }

        public OSCDTO? Get(string cnpj)
        {
            var result = _oscRepository.Get(cnpj);

            if (result == null) return null;

            var osc = new OSCDTO()
            {
                cnpj = result.Cnpj,
                Nome = result.Nome
            };

            foreach (var item in result.Contatos)
            {
                var itemDTO = new ContatoOscDTO
                {
                    Id = item.Id,
                    Contato = item.Contato,
                    Tipo = item.Tipo
                };

                osc.Contatos.Add(itemDTO);
            }

            foreach (var item in result.RepresentantesLegais)
            {
                var itemDTO = new RepresentanteLegalDTO
                {
                    Cpf = item.Cpf,
                    Nome = item.Nome,
                    email = item.email,
                    telefone = item.telefone
                };

                osc.RepresentanteLegal.Add(itemDTO);
            }

            foreach (var item in result.Localizacoes)
            {
                var itemDTO = new LocalizacaoDTO
                {
                    Cep = item.Cep,
                    Complemento = item.Complemento,
                    Distrito = item.Distrito,
                    Id = item.Id,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Logradouro = item.Logradouro,
                    Numero = item.Numero,
                    Regiao = item.Regiao,
                    SubPrefeitura = item.SubPrefeitura
                };

                osc.Localizacoes.Add(itemDTO);
            }

            return osc;
        }

        public bool Save(OSCDTO osc)
        {
            if (osc == null) return false;

            var newOsc = new OSC
            {
                Cnpj = osc.cnpj,
                Nome = osc.Nome,
                Localizacoes = new List<LocalizacaoOSC>(),
                RepresentantesLegais = new List<RepresentanteLegal>(),
                projetos = new List<Termo>(),
                Contatos = new List<ContatoOSC>()
            };

            foreach (var contatoDto in osc.Contatos)
            {
                var contatoModel = new ContatoOSC
                {
                    Contato = contatoDto.Contato,
                    Tipo = contatoDto.Tipo,
                    OSC = newOsc
                };

                newOsc.Contatos.Add(contatoModel);
            }

            foreach (var locDto in osc.Localizacoes)
            {
                var locModel = new LocalizacaoOSC
                {
                    Cep = locDto.Cep,
                    OSC = newOsc,
                    Complemento = locDto.Complemento,
                    Distrito = locDto.Distrito,
                    Latitude = locDto.Latitude,
                    Longitude = locDto.Longitude,
                    Logradouro = locDto.Logradouro,
                    Numero = locDto.Numero,
                    SubPrefeitura = locDto.SubPrefeitura,
                    Regiao = locDto.Regiao
                };

                newOsc.Localizacoes.Add(locModel);
            }

            foreach (var representante in osc.RepresentanteLegal)
            {
                var repModel = new RepresentanteLegal
                {
                    Cpf = representante.Cpf,
                    Nome = representante.Nome,
                    telefone = representante.telefone,
                    email = representante.email,
                    OSC = newOsc
                };

                newOsc.RepresentantesLegais.Add(repModel);
            }

            return _oscRepository.Save(newOsc);

        }
    }
}
