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
        public Tableau Tableau { get; }
        public Foundations Foundations { get; } = new Foundations();
        public CardStack[] GamePiles { get; } = new CardStack[7];
        private readonly Random Random = new Random();

        public Deck()
        {
            var cards = Enumerable.Range(0, 52)
                .Select(x => (Weight: Random.Next(), Card: new Card(x)))
                .OrderBy(x => x.Weight)
                .Select(c => c.Card);
            Tableau = new Tableau(cards);

            for (int index = 0; index < GamePiles.Count(); index++)
                GamePiles[index] = new CardStack();
        }

        public void DealCards()
        {
            for (int pileNumber = 0; pileNumber < 7; pileNumber++)
                for (int index = 0; index < pileNumber + 1; index++)
                {
                    Tableau.TableauSource.Peek().IsFaceUp = index == pileNumber;
                    GamePiles[pileNumber].Push(Tableau.TableauSource.Pop());
                }
        }
        public BaseStack GetStack(Card card)
        {
            //GamePiles
            foreach (CardStack stack in GamePiles)
                if (stack.Contains(card))
                    return stack;
            //Foundations
            if (Foundations.Club.Contains(card))
                return Foundations.Club;
            else if (Foundations.Spade.Contains(card))
                return Foundations.Spade;
            else if (Foundations.Heart.Contains(card))
                return Foundations.Heart;
            else if (Foundations.Diamond.Contains(card))
                return Foundations.Diamond;
            //Tableau
            if (Tableau.TableauGraveyard.Contains(card))
                return Tableau.TableauGraveyard;

            return null;
        }
    }
}
