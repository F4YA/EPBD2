using API_Interna.Data.Models;

namespace API_Interna.Interfaces.Repositorio
{
    public interface IOSCRepository
    {
        OSC? Get(string cnpj);
        OSC GetTodas();
        bool Save(OSC obj);
        bool Delete(OSC obj);
        bool Update(OSC obj);
    }
}
