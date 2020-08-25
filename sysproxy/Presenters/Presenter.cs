using System;
using sysproxy.Views;

namespace sysproxy.Presenters
{
    public class Presenter<TView> 
        where TView : class,IView
    {
        public TView View { get; private set; }
    
        public Presenter(TView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            View = view;
            view.LoadData += View_LoadData;
            view.SaveData += View_SaveData;
        }
       
        protected virtual void View_LoadData(object sender, EventArgs e)
        {
            
        }
        protected virtual void View_SaveData(object sender, EventArgs e)
        {

        }
    }
}
