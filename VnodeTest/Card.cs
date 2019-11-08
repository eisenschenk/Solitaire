using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    public class Card
    {
        public Style Color;
        public CardModel CardSprite;
        public PipModel PipSprite;
        public bool IsFlipped;
        public int PipID;
        public int CardDeckIndex;


        public Card(int cardDeckIndex)
        {
            CardDeckIndex = cardDeckIndex;
            PipID = cardDeckIndex / 13;
            if (PipID / 2 == 0)
                Color = Styles.TCblack;
            else
                Color = Styles.TCred;

            CardSprite = GetCardSprite(cardDeckIndex % 13);
            PipSprite = GetPipSprite();

        }
        private CardModel GetCardSprite(int cardID)
        {
            switch (cardID)
            {
                case 0: return CardModel.Ace;
                case 1: return CardModel.Two;
                case 2: return CardModel.Three;
                case 3: return CardModel.Four;
                case 4: return CardModel.Five;
                case 5: return CardModel.Six;
                case 6: return CardModel.Seven;
                case 7: return CardModel.Eight;
                case 8: return CardModel.Nine;
                case 9: return CardModel.Ten;
                case 10: return CardModel.Jack;
                case 11: return CardModel.Queen;
                case 12: return CardModel.King;
                default: return CardModel.Zero;
            }
        }
        private PipModel GetPipSprite()
        {
            switch (PipID)
            {
                case 0: return PipModel.Club;
                case 1: return PipModel.Spade;
                case 2: return PipModel.Heart;
                case 3: return PipModel.Diamond;
                default: return PipModel.Zero;
            }
        }
        public enum PipModel { Club, Spade, Heart, Diamond, Zero }
        public enum CardModel
        {
            Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Zero
        }


      
    }
}
