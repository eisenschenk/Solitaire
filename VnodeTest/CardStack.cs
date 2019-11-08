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


        public CardStack(List<Card> collection)
        {
            foreach (Card card in collection)
                CardPile.Push(card);
        }
        public CardStack()
        {

        }



    }
}
