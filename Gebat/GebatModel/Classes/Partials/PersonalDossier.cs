using System;

namespace GebatModel
{
    public partial class PersonalDossier
    {
        private int income = 0;

        #region//Private methods

        private void addIncome(int income)
        {
            this.income += income;
        }

        private Familiar[] getFamiliars()
        {
            Familiar[] familiars = new Familiar[this.Familiar.Count];
            this.Familiar.CopyTo(familiars, 0);
            return familiars;
        }

        #endregion

        #region//Public methods

        /// <summary>
        /// Gets the income for the PersonalDossier
        /// </summary>
        public int TotalIncome
        {
            get
            {
                if (income == 0)
                {
                    Familiar[] familiars = getFamiliars();
                    foreach (Familiar familiar in familiars)
                    {
                        addIncome(familiar.Income);
                    }
                }
                return income;
            }
        }

        #endregion
    }
}
