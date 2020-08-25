using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sysproxy.Views
{
    public interface IView
    {
        event EventHandler LoadData;
        event EventHandler SaveData;
    }
}
