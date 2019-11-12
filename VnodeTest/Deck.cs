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
        public Tableau Tableau;
        public Foundations Foundations = new Foundations();
        public CardStack[] GamePiles = new CardStack[7];
        private Random random = new Random();

        public Deck(int count)
        {
            var cards = Enumerable.Range(0, 52)
                .Select(x => (Weight: random.Next(), Card: new Card(x)))
                .OrderBy(x => x.Weight)
                .Select(c => c.Card);
            Tableau = new Tableau(cards);

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
                        Tableau.TableauSource.Peek().IsFlipped = true;
                    else
                        Tableau.TableauSource.Peek().IsFlipped = false;
                    GamePiles[pileNumber].Push(Tableau.TableauSource.Pop());
                }
        }


    }
}
