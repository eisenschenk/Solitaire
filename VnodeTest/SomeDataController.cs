using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static ACL.UI.React.DOM;

namespace VnodeTest
{
    public class SomeDataController
    {
        string username;
        string password;
        string userdata;
        string registerkarte1 = "RegisterKarte 1";
        string registerkarte2 = "RegisterKarte 2";
        string registerkarte3 = "RegisterKarte 3";
        string[] registerarray = { "*(T_T)* Karte1 *(T_T)*", "*(-_-)* Karte2 *(-_-)*", "*(O_o)* Karte3 *(O_o)*" };
        string[] searchArray = { "peter", "pan", "paul", "potz", "peter", "petra", "paula", "phil" };
        Func<VNode> registerRenderFunc;
        VNode RefreshReference;

        public SomeDataController()
        {
            DateTime nextRefresh = DateTime.Now;
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    RefreshReference?.Refresh();
                    Thread.Sleep((int)Math.Max(0, (nextRefresh - DateTime.Now).TotalMilliseconds));
                    nextRefresh = nextRefresh.AddSeconds(1);
                }
            });
        }

        public VNode Render()
        {
            return Div();


        }
    }
}
