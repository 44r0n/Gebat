using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class TypeRepository : BaseRepository , ITypeRepository
    {
        
        public List<Type> GetAllTypes()
        {
            return this.GetAll<Type>().ToList();
        }

        
        public void SaveType(Type type)
        {
            this.Save(type);
        }

        
        public void UpdateType(Type type)
        {
            this.Update(type);
        }

        
        public void DeleteType(Type type)
        {
            this.Delete(type);
        }

        
        public List<Type> SearchType(string name)
        {
            return this.GetAll<Type>().Where(type => type.Name.Contains(name)).ToList();
        }
    }
}
