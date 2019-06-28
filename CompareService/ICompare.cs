using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareService
{
    public interface ICompare
    {
        CompareResult Start(string userName, CompareInput compareInput);
        List<CompareResult> GetCompareHistory(string userName, int start, int count);
    }
}
