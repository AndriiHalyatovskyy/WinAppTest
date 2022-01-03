using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppTest.Pages
{
    public abstract class APage<T>
    {
        protected Page page;
        protected T selectors;

        public T Selectors
        {
            get { return selectors; }
        }

        protected APage(Page p, T t)
        {
            page = p;
            selectors = t;
        }
    }
}
