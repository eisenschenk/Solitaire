using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardDeck
    {
        public CardStack Deck = new CardStack();
        public CardStack Graveyard = new CardStack();
        public List<CardStack> Foundations = new List<CardStack>();
        public List<CardStack> GamePiles = new List<CardStack>();

        public CardDeck(int count)
        {
            for (int index = 0; index < 52; index++)
                Deck.CardPile.Push(new Card(index));
            for (int index = 0; index < 7; index++)
                GamePiles.Add(new CardStack());
            for (int index = 0; index < 4; index++)
                Foundations.Add(new CardStack());

        }

        public enum GamePileID { One, Two, Three, Four, Five, Six, Seven }
    }
}
