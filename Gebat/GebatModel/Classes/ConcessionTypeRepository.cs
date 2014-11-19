using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class ConcessionTypeRepository : BaseRepository , IConcessionTypeRepository
    {
        
        public List<ConcessionType> GetAllConcessionTypes()
        {
            return this.GetAll<ConcessionType>().ToList();
        }

        
        public void SaveConcessionType(ConcessionType type)
        {
            this.Save(type);
        }

        
        public void UpdateConcessionType(ConcessionType type)
        {
            this.Update(type);
        }

        
        public void DeleteConcessionType(ConcessionType type)
        {
            this.Delete(type);
        }

       
        public List<ConcessionType> SearchConcessionType(string name)
        {
            return this.GetAll<ConcessionType>().Where(concession => concession.Name.Contains(name)).ToList();
        }
    }
}
