﻿using ACL.UI.React;
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
        public CardModel CardValue;
        public PipModel PipValue;
        public string CardSprite;
        public string PipSprite;
        public bool IsFlipped;
        public bool IsSelected;
        private int PipID;


        public Card(int cardDeckIndex)
        {
            PipID = cardDeckIndex / 13;
            if (PipID / 2 == 0)
                Color = Styles.TCblack;
            else
                Color = Styles.TCred;

            CardValue = GetCardValue(cardDeckIndex % 13);
            CardSprite = GetValueSprite();
            PipValue = GetPipValue();
            PipSprite = GetPipSprite();

        }
        private CardModel GetCardValue(int cardID)
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
        private PipModel GetPipValue()
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

        private string GetValueSprite()
        {
            switch (CardValue)
            {
                case CardModel.Ace: return "A";
                case CardModel.Two: return "2";
                case CardModel.Three: return "3";
                case CardModel.Four: return "4";
                case CardModel.Five: return "5";
                case CardModel.Six: return "6";
                case CardModel.Seven: return "7";
                case CardModel.Eight: return "8";
                case CardModel.Nine: return "9";
                case CardModel.Ten: return "10";
                case CardModel.Jack: return "J";
                case CardModel.Queen: return "Q";
                case CardModel.King: return "K";
                default: return "0";
            }
        }

        private string GetPipSprite()
        {
            switch (PipValue)
            {
                case PipModel.Club: return "♣";
                case PipModel.Spade: return "♠";
                case PipModel.Heart: return "♥";
                case PipModel.Diamond: return "♦";
                default: return "0";
            }
        }

        public void Click(CardStack source, CardStack target)
        {
            if (IsSelected == false && IsFlipped)
                IsSelected = true;
            else if (IsSelected == true)
                IsSelected = false;
            else
                source.TryMove(target);
        }

        public CardStack TryGetStack(Deck deck)
        {
            foreach (CardStack stack in deck.GamePiles)
                if (stack.CardPile.Contains(this))
                    return stack;
            if (deck.Foundations.Club.CardPile.Contains(this))
                return deck.Foundations.Club;
            else if (deck.Foundations.Spade.CardPile.Contains(this))
                return deck.Foundations.Spade;
            else if (deck.Foundations.Heart.CardPile.Contains(this))
                return deck.Foundations.Heart;
            else if (deck.Foundations.Diamond.CardPile.Contains(this))
                return deck.Foundations.Diamond;
            else
                return null;
        }

    }
}
