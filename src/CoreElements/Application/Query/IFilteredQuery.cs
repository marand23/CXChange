using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.CoreElements.Application.Query
{
    public interface IFilteredQuery
    {
        List<string> Filters { get; }
    }
}
