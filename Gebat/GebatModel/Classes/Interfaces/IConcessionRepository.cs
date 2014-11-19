using System.Collections.Generic;

namespace GebatModel
{
    public interface IConcessionRepository
    {
        
        List<Concession> GetAllConcessions();

        
        void UpdateConcession(Concession concession);

        
        void DeleteConcession(Concession concession);
    }
}
