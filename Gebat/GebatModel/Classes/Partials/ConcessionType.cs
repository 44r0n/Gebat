using System.Collections.Generic;

namespace GebatModel
{
    public partial class ConcessionType
    {
        /// <summary>
        /// Checks if the concession type for a given concession.
        /// </summary>
        /// <returns>Boolan.</returns>
        /// <param name="concession">Concession to Check.</param>
        public bool CheckAll(Concession concession)
        {
            bool ret = true;
            ret = DateRestriction.IsValid(concession);
            return ret;
        }
    }
}
