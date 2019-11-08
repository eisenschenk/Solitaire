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
        public List<Card> Deck = new List<Card>();
        public List<CardStack> Foundations = new List<CardStack>();
        public List<CardStack> GamePiles = new List<CardStack>();

        public CardDeck(int count)
        {
            //enums
            for (int index = 0; index < 52; index++)
                Deck.Add(new Card(index));
            for (int index = 0; index < 7; index++)
                GamePiles.Add(new CardStack());
            for (int index = 0; index < 4; index++)
                Foundations.Add(new CardStack());

        }

        public enum GamePileID { One, Two, Three, Four, Five, Six, Seven }
    }
}
