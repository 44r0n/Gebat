using System.Collections.Generic;

namespace GebatModel
{
    public interface ITypeRepository
    {
        
        List<Type> GetAllTypes();

        
        void SaveType(Type type);

        
        void UpdateType(Type type);

        
        void DeleteType(Type type);

        
        List<Type> SearchType(string name);
    }
}
