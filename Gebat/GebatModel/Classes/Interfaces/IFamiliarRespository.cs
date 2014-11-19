using System.Collections.Generic;

namespace GebatModel
{
    public interface IFamiliarRespository
    {
        
        List<Familiar> GetAllFamiliars();

        
        void UpdateFamiliar(Familiar familiar);

        
        void DeleteFamiliar(Familiar familiar);

       
        List<Familiar> SearchFamiliar(string DNI);
    }
}
