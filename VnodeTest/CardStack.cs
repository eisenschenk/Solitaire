using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardStack
    {
        public Stack<Card> CardPile = new Stack<Card>();

        public CardStack(IEnumerable<Card> collection)
        {
            CardPile = new Stack<Card>(collection);
        }
        public CardStack()
        {
        }



    }
}
