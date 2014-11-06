using System;

namespace GebatModel
{
    public partial class Concession
    {
        private static DateTime today = DateTime.MinValue;

        #region//Private Methods

        private bool valid(int interval, DateTime date)
        {
            int diff = (date.Month - FinishDate.Month) + 12 * (date.Year - FinishDate.Year);
            /*if (this.Concatenable && !())
            {
                
            }*/
            return (diff >= interval);
        }

        private DateRestriction getDateRestriction()
        {
            return this.Type.DateRestriction;
        }

        #endregion

        #region//Public Methods

        public static void SetDate(DateTime date)
        {
            today = date;
        }

        /// <summary>
        /// Checks if current concession is valid.
        /// </summary>
        /// <returns>Bolean.</returns>
        public bool IsValid()
        {
            DateRestriction dateRestiction = getDateRestriction();
            int interval = dateRestiction.Interval;
            if (today == DateTime.MinValue)
            {
                return valid(interval, DateTime.Now);
            }
            else
            {
                return valid(dateRestiction.Interval, today);
            }
        }

        #endregion
    }
}
