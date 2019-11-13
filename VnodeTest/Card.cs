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
        public Style Color { get; }
        public CardModel CardValue { get; }
        public PipModel PipValue { get; }
        public string CardSprite { get; }
        public string PipSprite { get; }
        public bool IsFaceUp { get; set; }
        public Card(int cardDeckIndex)
        {
            CardValue = GetCardValue(cardDeckIndex % 13);
            CardSprite = GetValueSprite();
            PipValue = GetPipValue(cardDeckIndex / 13);
            PipSprite = GetPipSprite();
            if ((int)PipValue / 2 == 0)
                Color = Styles.TCblack;
            else
                Color = Styles.TCred;
        }
        private CardModel GetCardValue(int cardID)
        {
            return cardID switch
            {
                0 => CardModel.Ace,
                1 => CardModel.Two,
                2 => CardModel.Three,
                3 => CardModel.Four,
                4 => CardModel.Five,
                5 => CardModel.Six,
                6 => CardModel.Seven,
                7 => CardModel.Eight,
                8 => CardModel.Nine,
                9 => CardModel.Ten,
                10 => CardModel.Jack,
                11 => CardModel.Queen,
                12 => CardModel.King,
                _ => CardModel.Zero,
            };
        }
        private PipModel GetPipValue(int pipID)
        {
            return pipID switch
            {
                0 => PipModel.Club,
                1 => PipModel.Spade,
                2 => PipModel.Heart,
                3 => PipModel.Diamond,
                _ => PipModel.Zero,
            };
        }                

        private string GetValueSprite()
        {

            return CardValue switch
            {
                CardModel.Ace => "A",
                CardModel.Two => "2",
                CardModel.Three => "3",
                CardModel.Four => "4",
                CardModel.Five => "5",
                CardModel.Six => "6",
                CardModel.Seven => "7",
                CardModel.Eight => "8",
                CardModel.Nine => "9",
                CardModel.Ten => "10",
                CardModel.Jack => "J",
                CardModel.Queen => "Q",
                CardModel.King => "K",
                _ => "0",
            };
        }

        private string GetPipSprite()
        {
            return PipValue switch
            {
                PipModel.Club => "♣",
                PipModel.Spade => "♠",
                PipModel.Heart => "♥",
                PipModel.Diamond => "♦",
                _ => "0",
            };
        }



        //TODO => deck
        public BaseStack GetStack(Deck deck)
        {
            //GamePiles
            foreach (CardStack stack in deck.GamePiles)
                if (stack.Contains(this))
                    return stack;
            //Foundations
            if (deck.Foundations.Club.Contains(this))
                return deck.Foundations.Club;
            else if (deck.Foundations.Spade.Contains(this))
                return deck.Foundations.Spade;
            else if (deck.Foundations.Heart.Contains(this))
                return deck.Foundations.Heart;
            else if (deck.Foundations.Diamond.Contains(this))
                return deck.Foundations.Diamond;
            //Tableau
            if (deck.Tableau.TableauGraveyard.Contains(this))
                return deck.Tableau.TableauGraveyard;

            return null;
        }

    }
}
