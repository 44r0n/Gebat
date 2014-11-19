using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class TBCRepository : BaseRepository, ITBCRepository
    {
        public List<TBC> GetAllTBC()
        {
            return this.GetAll<TBC>().ToList();
        }

        public void SaveTBC(TBC tbc)
        {
            this.Save(tbc);
        }

        public void UpdateTBC(TBC tbc)
        {
            this.Update(tbc);
        }

        public void DeleteTBC(TBC tbc)
        {
            this.Delete(tbc);
        }

        public List<TBC> SeacrhTBC(string dni)
        {
            return this.GetAll<TBC>().Where(tbc => tbc.DNI.Contains(dni)).ToList();
        }
    }
}
