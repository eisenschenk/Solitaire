using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public abstract class BaseStack : Stack<Card>
    {

        public bool TryPush(BaseStack source)
        {
            if (CanPush())
                source.TryMove();

        }
        public abstract bool CanPush();
    }
}
