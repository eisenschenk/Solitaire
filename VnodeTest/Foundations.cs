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
    public FoundationStack Club = new FoundationStack(PipModel.Club);
    public FoundationStack Spade = new FoundationStack(PipModel.Spade);
    public FoundationStack Heart = new FoundationStack(PipModel.Heart);
    public FoundationStack Diamond = new FoundationStack(PipModel.Diamond);

        public Foundations()
        {
        }
        public enum PipModel { Club, Spade, Heart, Diamond, Zero }

    }
}
