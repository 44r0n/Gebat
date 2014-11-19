using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class CrimeRepository : BaseRepository , ICrimeRepository
    {
        public List<Crime> GetAllCrimes()
        {
            return this.GetAll<Crime>().ToList();
        }

        public void SaveCrime(Crime crime)
        {
            this.Save(crime);
        }

        public void UpdateCrime(Crime crime)
        {
            this.Update(crime);
        }

        public void DeleteCrime(Crime crime)
        {
            this.Delete(crime);
        }

        public List<Crime> SearchCrimeByName(string name)
        {
            return this.GetAll<Crime>().Where(crime => crime.Name.Contains(name)).ToList();
        }
    }
}
