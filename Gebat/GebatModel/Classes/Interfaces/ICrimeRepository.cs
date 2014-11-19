using System.Collections.Generic;

namespace GebatModel
{
    public interface ICrimeRepository
    {
        List<Crime> GetAllCrimes();

        void SaveCrime(Crime crime);

        void UpdateCrime(Crime crime);

        void DeleteCrime(Crime crime);

        List<Crime> SearchCrimeByName(string name);
    }
}
