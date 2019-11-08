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
        public CardDeck Cards = new CardDeck(52);
        public Card CurrentCard;
        private Card IsSelected;
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
                    if (index == pileNumber)
                        Cards.Deck.CardPile.Peek().IsFlipped = true;
                    else
                        Cards.Deck.CardPile.Peek().IsFlipped = false;
                    Cards.GamePiles[pileNumber].CardPile.Push(Cards.Deck.CardPile.Pop());
                }
        }
        public VNode Render()
        {
            return Div
                (
                Row(
                    Row(
                        Styles.FitContent & Styles.W33,
                        Div(() => NextCard(), RenderCardback(Styles.CardGreen)),
                        RenderCard(Cards.Graveyard.CardPile.Peek())
                        ),
                    RenderFoundationPiles()
                    ),
                RenderGamePiles()
                );


        }
        private void NextCard()
        {
            if (Cards.Deck.CardPile.Count() != 0)
                Cards.Graveyard.CardPile.Push(Cards.Deck.CardPile.Pop());
            else
                foreach (Card card in Cards.Graveyard.CardPile)
                    Cards.Deck.CardPile.Push(Cards.Graveyard.CardPile.Pop());
        }
        private VNode RenderFoundationPiles()
        {
            return Row
                (
                Cards.Foundations[0].CardPile.Count != 0 ? RenderCard(Cards.Foundations[0].CardPile.Peek()) : RenderCardback(Styles.CardBlack, "Club"),
                Cards.Foundations[1].CardPile.Count != 0 ? RenderCard(Cards.Foundations[1].CardPile.Peek()) : RenderCardback(Styles.CardBlack, "Spade"),
                Cards.Foundations[2].CardPile.Count != 0 ? RenderCard(Cards.Foundations[2].CardPile.Peek()) : RenderCardback(Styles.CardRed, "Heart"),
                Cards.Foundations[3].CardPile.Count != 0 ? RenderCard(Cards.Foundations[3].CardPile.Peek()) : RenderCardback(Styles.CardRed, "Diamond")
                );
        }
        private VNode RenderGamePiles()
        {
            VNode RenderGamePile(CardStack stack)
            {
                if (stack.CardPile.Count != 0)
                    return Col(
                         Fragment(stack.CardPile.Where(c => c.IsFlipped == false).Select(c => RenderCardbackTop())),
                         Fragment(stack.CardPile.Where(c => c.IsFlipped == true && c != stack.CardPile.Peek()).Select(c => RenderCardTopPart(c))),
                         RenderCard(stack.CardPile.Peek())
                     );
                else
                    return RenderEmptyCard();
            }
            return Row(Cards.GamePiles.Select(p => RenderGamePile(p)));
        }
        private VNode RenderCard(Card card)
        {
            var div = Div(
                Styles.BorderedBoxBlack & Styles.W4C & Styles.M2,
                RenderCardTopPart(card),
                RenderCardBody(card),
                RenderCardBottomPart(card)
            );
            div.OnClick = () => ClickCard(card);
            //div.Drag = new DragInfo(DragMode.Source);
            //div.OnDrag = DragCard;

            return div;
        }

        private void DragCard(DragContext context, DragAction action, VNode target)
        {
            switch (action)
            {
                case DragAction.Drag:
                    RemoveCard(CurrentCard);


                    break;

                case DragAction.Drop:

                    break;
            }

        }

        private VNode RenderCardTopPart(Card card)
        {
            return Row(
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
            return Row(
                Styles.W4C,
                Text($"{GetPipSprite(card)}", card.Color & Styles.W2C),
                Text($"{GetCardSprite(card)}", card.Color & Styles.TextAlignR & Styles.W2C)
            );
        }

        public static VNode RenderCardback(Style color, string title = "Deck")
        {
            return Div(
                Styles.BorderedBox & Styles.W4C & Styles.M2 & color,
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text(title, Styles.W4C & Styles.TextAlignC),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C)
            );
        }
        public static VNode RenderCardbackTop()
        {
            return Div(
                Styles.CardBackPartial & Styles.W4C & Styles.M2,
                Text("XXXXX", Styles.TextAlignC & Styles.W4C)
            );
        }
        private static VNode RenderEmptyCard()
        {
            return Div(Styles.CardEmptyBorderGreen);
        }

        private string GetCardSprite(Card card)
        {
            switch (card.CardSprite)
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

        private void ClickCard(Card card)
        {
            if (IsSelected == null && card.IsFlipped)
                IsSelected = card;
            else if (IsSelected == card)
                IsSelected = null;
            else
                MoveSelected(card, GetCardPile(card));
        }
        //TODO
        private void MoveSelected(Card source, CardStack target)
        {
            if (target == Cards.Foundations[source.PipID])
            {

            }
            var isCardInPile = Cards.GamePiles.Where(s => s.CardPile.Contains(source)).
            if (target.CardPile.Contains(source))

                if (Cards.Foundations[source.PipID].CardPile.Count == 0 && source.CardSprite == 0)
                {
                    RemoveCard(source);
                    Cards.Foundations[source.PipID].CardPile.Push(source);
                    return;
                }
            if (Cards.Foundations[source.PipID].CardPile.Count != 0 && Cards.Foundations[source.PipID].CardPile.Peek().CardDeckIndex == source.CardDeckIndex - 1)
            {
                RemoveCard(source);
                Cards.Foundations[source.PipID].CardPile.Push(source);
            }
        }
        private void RemoveCard(Card card)
        {
            if (Cards.Graveyard.CardPile.Peek() == card)
                Cards.Graveyard.CardPile.Pop();
            foreach (CardStack stack in Cards.Foundations)
                if (stack.CardPile.Peek() == card)
                    stack.CardPile.Pop();
            foreach (CardStack stack in Cards.GamePiles)
                if (stack.CardPile.Peek() == card)
                    stack.CardPile.Pop();
        }
        private void Drag(Card card)
        {

        }
    }
}
