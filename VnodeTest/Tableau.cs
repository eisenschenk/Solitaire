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
                TableauGraveyard.CardPile.Push(TableauSource.CardPile.Pop());
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
