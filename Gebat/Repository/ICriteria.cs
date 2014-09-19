using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICriteria
    {
        public bool IsSatisfiedBy(object candidate);
    }

    public interface ICompositeCriteria : ICriteria
    {
        public ICriteria And(ICriteria other);
        public ICriteria Or(ICriteria other);
        public ICriteria Not();
    }
}
