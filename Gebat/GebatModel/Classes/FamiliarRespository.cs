using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class FamiliarRespository : BaseRepository , IFamiliarRespository
    {
        
        public List<Familiar> GetAllFamiliars()
        {
            return this.GetAll<Familiar>().ToList();
        }

        
        public void UpdateFamiliar(Familiar familiar)
        {
            this.Update(familiar);
        }

        
        public void DeleteFamiliar(Familiar familiar)
        {
            this.Delete(familiar);
        }

        
        public List<Familiar> SearchFamiliar(string DNI)
        {
            return this.GetAll<Familiar>().Where(familiar => familiar.DNI.Contains(DNI)).ToList();
        }
    }
}
