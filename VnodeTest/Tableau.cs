using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Tableau
    {
        public CardStack TableauSource = new CardStack();
        public CardStack TableauGraveyard = new CardStack();
        public void NextCard()
        {
            if (TableauSource.CardPile.Count != 0)
            {
                if (!TableauGraveyard.IsEmpty)
                    TableauGraveyard.CardPile.Peek().IsFlipped = false;
                TableauGraveyard.CardPile.Push(TableauSource.CardPile.Pop());
                TableauGraveyard.CardPile.Peek().IsFlipped = true;
                TableauGraveyard.IsEmpty = false;
            }
            else
            {
                while (TableauGraveyard.CardPile.Count != 0)
                    TableauSource.CardPile.Push(TableauGraveyard.CardPile.Pop());
                TableauGraveyard.IsEmpty = true;
                TableauSource.IsEmpty = false;
            }
        }


        public void ShuffleDeck()
        {

        }
    }
}
