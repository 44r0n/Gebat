namespace GebatModel
{
    public interface IRestriction
    {
        /// <summary>
        /// Checks if current restriction is valid in the given concession.
        /// </summary>
        /// <param name="concession">Concession to check.</param>
        /// <returns>Boolean.</returns>
        bool IsValid(Concession concession);
    }
}
