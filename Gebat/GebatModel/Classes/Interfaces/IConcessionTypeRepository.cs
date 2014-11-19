using System.Collections.Generic;

namespace GebatModel
{
    public interface IConcessionTypeRepository
    {
        
        List<ConcessionType> GetAllConcessionTypes();

        
        void SaveConcessionType(ConcessionType type);

        
        void UpdateConcessionType(ConcessionType type);

        
        void DeleteConcessionType(ConcessionType type);

        
        List<ConcessionType> SearchConcessionType(string name);
    }
}
