using API_Interna.DTOs;

namespace API_Interna.Interfaces.Business
{
    public interface IOSCBusiness
    {
        public OSCDTO? Get(string cnpj);
        public bool Save(OSCDTO osc);    
    }
}
