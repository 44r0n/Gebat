using System.Collections.Generic;

namespace GebatModel
{
    public interface ITBCRepository
    {
        List<TBC> GetAllTBC();

        void SaveTBC(TBC tbc);

        void UpdateTBC(TBC tbc);

        void DeleteTBC(TBC tbc);

        List<TBC> SearchTBC(string DNI);
    }
}
