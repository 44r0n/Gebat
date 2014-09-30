﻿using System;

namespace GebatModel
{
    public partial class DateRestriction : IRestriction
    {
        private static DateTime today = DateTime.MinValue;

        public static void SetDate(DateTime date)
        {
            today = date;
        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="interval">Interval of months.</param>
        /// <param name="concatenable">Check if it is concatenable one month.</param>
        public DateRestriction(int interval, bool concatenable = false)
        {
            this.Interval = interval;
            this.Concatenable = concatenable;
        }

        private bool valid(Concession concession, DateTime date)
        {
            int diff = (date.Month - concession.FinishDate.Month) + 12 * (date.Year - concession.FinishDate.Year);
            return (diff >= Interval);
        }

        /// <summary>
        /// Checks if current restriction is valid in the given concession.
        /// </summary>
        /// <param name="concession">Concession to check.</param>
        /// <returns>Boolean.</returns>
        public bool IsValid(Concession concession)
        {
            if(today == DateTime.MinValue)
            {
                return this.valid(concession, DateTime.Now);
            }
            else
            {
                return this.valid(concession,today);
            }
        }
    }
}
