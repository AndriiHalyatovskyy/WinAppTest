using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppTest.Pages.Calc
{
    public class CalcPage : APage<CalcPageSelectors>
    {
        public CalcPage(Page p) : base(p, new CalcPageSelectors())
        {
        }
    }

    public class CalcPageSelectors
    {

    }
}
