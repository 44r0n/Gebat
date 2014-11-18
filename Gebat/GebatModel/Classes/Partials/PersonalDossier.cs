using System;

namespace GebatModel
{
    public partial class PersonalDossier
    {

        #region//Attributes

        private int income = 0;

        #endregion

        #region//Private methods

        //TODO: refactoriza, los métodos privados hacen más de una cosa, asegurate de que hagan una sola.

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

        private Concession[] getConcessions()
        {
            Concession[] concessions = new Concession[Concessions.Count];
            Concessions.CopyTo(concessions, 0);
            return concessions;
        }

        private Concession getLastConcession()
        {
            if(Concessions.Count == 0)
            {
                return null;
            }
            else
            {
                Concession[] concessions = getConcessions();
                return concessions[Concessions.Count - 1];
            }
        }

        private bool twoLastSame()
        {
            if(Concessions.Count == 1)
            {
                return false;
            }
            else
            {
                int maxConcessions = Concessions.Count;
                Concession[] concessions = getConcessions();
                return (concessions[maxConcessions - 1].Type == concessions[maxConcessions - 2].Type);
            }
        }

        private bool valid(int interval, Concession concession)
        {
            Concession lastConcession = getLastConcession();
            if (lastConcession == null)
            {
                return true;
            }
            else
            {
                return diffDates(interval, concession);
            }
        }

        private bool diffDates(int interval, Concession concession)
        {
            Concession lastConcession = getLastConcession();
            int diff = (concession.BeginDate.Month - lastConcession.FinishDate.Month) + 12 * (concession.BeginDate.Year - lastConcession.FinishDate.Year);
            if (diff >= interval)
            {
                return true;
            }
            else
            {
                return !(twoLastSame());
            }
        }

        private DateRestriction getDateRestriction(Concession concession)
        {
            return concession.Type.DateRestriction;
        }


        private bool isValid(Concession concession)
        {
            DateRestriction dateRestiction = getDateRestriction(concession);
            int interval = dateRestiction.Interval;
            if(interval == 0)
            {
                return true;
            }
            else
            {
                return valid(interval, concession);
            }
            
        }

        private void calculateIncome()
        {
            Familiar[] familiars = getFamiliars();
            foreach (Familiar familiar in familiars)
            {
                addIncome(familiar.Income);
            }
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
                    calculateIncome();
                }
                return income;
            }
        }

        /// <summary>
        /// Adds a concession to the dossier.
        /// </summary>
        /// <param name="concession">Concession to add.</param>
        public void AddConcession(Concession concession)
        {
            if (isValid(concession))
            {
                Concessions.Add(concession);
            }
            else
            {
                throw new ConcessionDateException("Check your dates");
            }
        }

        #endregion
    }
}
