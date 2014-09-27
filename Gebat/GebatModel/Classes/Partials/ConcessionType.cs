using System.Collections.Generic;

namespace GebatModel
{
    public partial class ConcessionType
    {
        /// <summary>
        /// Adds a restriction to the concession type.
        /// </summary>
        /// <param name="restriction">Restriction to add.</param>
        public void AddRestriction(IRestriction restriction)
        {
            //restrictions.Add(restriction);
        }

        /// <summary>
        /// Checks if the concession type for a given concession.
        /// </summary>
        /// <returns>Boolan.</returns>
        /// <param name="concession">Concession to Check.</param>
        public bool CheckAll(Concession concession)
        {
            bool ret = true;
            /*foreach(IRestriction res in restrictions)
            {
                if(!res.IsValid(concession))
                {
                    ret = false;
                    break;
                }
            }*/
            return ret;
        }
    }
}
