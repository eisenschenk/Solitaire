using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solitaire
{
    public class RootController
    {
        private readonly Session Session;
        private Func<VNode> CurrentContent;

        public RootController(Session session)
        {
            Session = session;
        }

        //public VNode Render() => CurrentContent();
        public VNode Render() => SomeDataController.Render();

        private GameBoard _SomeDataController;
        private GameBoard SomeDataController =>
        _SomeDataController ??
        (_SomeDataController = ((Application)Application.Instance).AppContext.CreateSomeDataController());
    }

}
