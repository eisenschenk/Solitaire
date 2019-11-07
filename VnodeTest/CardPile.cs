using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardPile
    {
        public List<Card> Deck = new List<Card>();

        public List<Card>[] GamePiles = new List<Card>[7];

        public List<Card> FoundationpileClub = new List<Card>();
        public List<Card> FoundationpileSpade = new List<Card>();
        public List<Card> FoundationpileHeart = new List<Card>();
        public List<Card> FoundationpileDiamonds = new List<Card>();
       
        public CardPile(int count)
        {
            //enums
            for (int index = 0; index < 52; index++)
                Deck.Add(new Card(index));

            GamePiles[0] = new List<Card>();
            GamePiles[1] = new List<Card>();
            GamePiles[2] = new List<Card>();
            GamePiles[3] = new List<Card>();
            GamePiles[4] = new List<Card>();
            GamePiles[5] = new List<Card>();
            GamePiles[6] = new List<Card>();
        }


    }
}
