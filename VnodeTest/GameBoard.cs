using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    public class GameBoard
    {
        public int CurrentCardIndex = 0;
        public CardPile CardDeck = new CardPile(52);
        public GameBoard()
        {
            ShuffleCardDeck();
            DealCards();
        }
        //TODO
        private void ShuffleCardDeck()
        {

        }
        private void DealCards()
        {
            for (int pileNumber = 0; pileNumber < 7; pileNumber++)
                for (int index = 0; index < pileNumber + 1; index++)
                {
                    CardDeck.GamePiles[pileNumber].Add(CardDeck.Deck[0]);
                    CardDeck.Deck.Remove(CardDeck.Deck[0]);
                }
        }
        public VNode Render()
        {
            return Div
                (
                Row(
                    Row(
                        Styles.W50,
                        Div(() => NextCard(), RenderCardback(Styles.CardGreen)),
                        RenderCard(CardDeck.Deck[CurrentCardIndex])
                        ),
                    RenderFoundationPiles()
                    ),
                RenderGamePiles()
                );


        }
        private void NextCard()
        {
            if (CurrentCardIndex < CardDeck.Deck.Count - 1)
                CurrentCardIndex++;
            else
                CurrentCardIndex = 0;
        }
        private VNode RenderFoundationPiles()
        {
            return Row
                (
                CardDeck.FoundationpileClub.Count != 0 ? RenderCard(CardDeck.FoundationpileClub[CardDeck.FoundationpileClub.Count - 1]) : RenderCardback(Styles.CardBlack, "Club"),
                CardDeck.FoundationpileSpade.Count != 0 ? RenderCard(CardDeck.FoundationpileSpade[CardDeck.FoundationpileSpade.Count - 1]) : RenderCardback(Styles.CardBlack, "Spade"),
                CardDeck.FoundationpileHeart.Count != 0 ? RenderCard(CardDeck.FoundationpileHeart[CardDeck.FoundationpileHeart.Count - 1]) : RenderCardback(Styles.CardRed, "Heart"),
                CardDeck.FoundationpileDiamonds.Count != 0 ? RenderCard(CardDeck.FoundationpileDiamonds[CardDeck.FoundationpileDiamonds.Count - 1]) : RenderCardback(Styles.CardRed, "Diamond")
                );
        }
        private VNode RenderGamePiles()
        {
            return Div();
            //    var x = CardDeck.Select((gamePile, index) => new { gamePile, index }).Where(index ;
            //    Row(
            //        RenderGamePile()
            //        );
        }
        private VNode RenderCard(Card card)
        {
            return Div
                (
                Styles.BorderedBoxBlack & Styles.W4C & Styles.M2,
                RenderCardTopPart(card),
                RenderCardBody(card),
                RenderCardBottomPart(card)
                );
        }
        private VNode RenderCardTopPart(Card card)
        {
            return Row
                (
                Styles.W4C,
                Text($"{GetCardSprite(card)}", card.Color & Styles.W2C),
                Text($"{GetPipSprite(card)}", card.Color & Styles.TextAlignR & Styles.W2C)
                );
        }
        private VNode RenderCardBody(Card card)
        {
            return Text($"{GetCardSprite(card)}", card.Color & Styles.TextAlignC & Styles.W4C & Styles.FontSize3);
        }
        private VNode RenderCardBottomPart(Card card)
        {
            return Row
                (
                Styles.W4C,
                Text($"{GetPipSprite(card)}", card.Color & Styles.W2C),
                Text($"{GetCardSprite(card)}", card.Color & Styles.TextAlignR & Styles.W2C)
                );
        }

        public static VNode RenderCardback(Style color, string title = "Deck")
        {
            return Div
                (
                Styles.BorderedBox & Styles.W4C & Styles.M2 & color,
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text(title, Styles.W4C & Styles.TextAlignC),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C)
                );
        }

        private string GetCardSprite(Card card)
        {
            switch(card.CardSprite)
            {
                case Card.CardModel.Ace: return "A";
                case Card.CardModel.Two: return "2";
                case Card.CardModel.Three: return "3";
                case Card.CardModel.Four: return "4";
                case Card.CardModel.Five: return "5";
                case Card.CardModel.Six: return "6";
                case Card.CardModel.Seven: return "7";
                case Card.CardModel.Eight: return "8";
                case Card.CardModel.Nine: return "9";
                case Card.CardModel.Ten: return "10";
                case Card.CardModel.Jack: return "J";
                case Card.CardModel.Queen: return "Q";
                case Card.CardModel.King: return "K";
                default: return "0";
            }
        }
        private string GetPipSprite(Card card)
        {
            switch (card.PipSprite)
            {
                case Card.PipModel.Club: return "♣";
                case Card.PipModel.Spade: return "♠";
                case Card.PipModel.Heart: return "♥";
                case Card.PipModel.Diamond: return "♦";
                default: return "0";
            }

        }

    }
}
