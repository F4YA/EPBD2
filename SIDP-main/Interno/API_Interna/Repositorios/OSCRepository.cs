using API_Interna.Data;
using API_Interna.Data.Models;
using API_Interna.Interfaces.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace API_Interna.Repositorios
{
    public class OSCRepository : IOSCRepository
    {
        private readonly DPContext _dpContext;
        public OSCRepository(DPContext dPContext)
        {
            _dpContext = dPContext;
        }
        public bool Delete(OSC obj)
        {
            throw new NotImplementedException();
        }

        public OSC? Get(string cnpj)
        {
            OSC osc = _dpContext.OSCs.Include(x => x.Contatos).Include(x => x.Localizacoes).Include(x => x.RepresentantesLegais).Single(x => x.Cnpj == cnpj);

            if (osc != null) return osc;

            return null;
        }

        public OSC GetTodas()
        {
            throw new NotImplementedException();
        }

        public bool Save(OSC obj)
        {
            _dpContext.OSCs.Add(obj);
            var result = _dpContext.SaveChanges(); //todo: Acredito que pode dar problema no futuro
            if(result != 0) return true;
            return false;
        }

        public bool Update(OSC obj)
        {
            throw new NotImplementedException();
        }
    }
}
