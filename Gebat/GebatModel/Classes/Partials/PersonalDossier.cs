using System;

namespace GebatModel
{
    public partial class PersonalDossier
    {
        public int TotalIncome
        {
            get
            {
                int income = 0;
                Familiar[] familiars = new Familiar[this.Familiar.Count];
                this.Familiar.CopyTo(familiars, 0);
                foreach(Familiar f in familiars)
                {
                    income += f.Income;
                }
                return income;
            }
        }
    }
}
