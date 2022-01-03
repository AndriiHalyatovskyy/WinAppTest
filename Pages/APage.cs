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
