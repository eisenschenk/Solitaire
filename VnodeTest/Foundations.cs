using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Foundations
    {
        public FoundationStack Club { get; } = new FoundationStack(PipModel.Club);
        public FoundationStack Spade { get; } = new FoundationStack(PipModel.Spade);
        public FoundationStack Heart { get; } = new FoundationStack(PipModel.Heart);
        public FoundationStack Diamond { get; } = new FoundationStack(PipModel.Diamond);
    }
}
