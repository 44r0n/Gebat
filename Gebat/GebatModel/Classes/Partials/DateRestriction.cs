namespace GebatModel
{
    public partial class DateRestriction : IRestriction
    {
        /// <summary>
        /// Checks if current restriction is valid in the given concession.
        /// </summary>
        /// <param name="concession">Concession to check.</param>
        /// <returns>Boolean.</returns>
        public bool IsValid(Concession concession)
        {
            return false;
        }
    }
}
