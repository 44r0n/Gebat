using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class ConcessionRepository : BaseRepository , IConcessionRepository
    {
        
        public List<Concession> GetAllConcessions()
        {
            return this.GetAll<Concession>().ToList();
        }

        
        public void UpdateConcession(Concession concession)
        {
            this.Update(concession);
        }

        
        public void DeleteConcession(Concession concession)
        {
            this.Delete(concession);
        }
    }
}
