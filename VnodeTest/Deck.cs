using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Deck
    {
        public Tableau Tableau = new Tableau();
        public Foundations Foundations = new Foundations();
        public CardStack[] GamePiles = new CardStack[7];

        public Deck(int count)
        {
            for (int index = 0; index < count; index++)
                Tableau.TableauSource.CardPile.Push(new Card(index));
            for (int index = 0; index < GamePiles.Count(); index++)
                GamePiles[index] = new CardStack();

        }

        public enum GamePileID { One, Two, Three, Four, Five, Six, Seven }

        public void DealCards()
        {
            for (int pileNumber = 0; pileNumber < 7; pileNumber++)
                for (int index = 0; index < pileNumber + 1; index++)
                {
                    if (index == pileNumber)
                        Tableau.TableauSource.CardPile.Peek().IsFlipped = true;
                    else
                        Tableau.TableauSource.CardPile.Peek().IsFlipped = false;
                    GamePiles[pileNumber].CardPile.Push(Tableau.TableauSource.CardPile.Pop());
                    GamePiles[pileNumber].IsEmpty = false;
                }
        }


    }
}
