using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    public class GameBoard
    {
        
        VNode RefreshReference;

        public GameBoard()
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
