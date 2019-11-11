using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    //TODO CLASSES
    public class GameBoard
    {
        public int CurrentCardIndex = 0;
        public Deck Cards = new Deck(52);
        public Card Selected;
        public GameBoard()
        {
            Cards.Tableau.ShuffleDeck();
            Cards.DealCards();
        }

        public VNode Render()
        {
            return Div(
                    Row(
                        Row(
                            Styles.FitContent & Styles.W33,
                            Div(() => Cards.Tableau.NextCard(), RenderCardback(Styles.CardGreen)),
                            Cards.Tableau.TableauGraveyard.IsEmpty ? RenderEmptyCard() : RenderCard(Cards.Tableau.TableauGraveyard.CardPile.Peek())
                        ),
                        RenderFoundationPiles()
                    ),
                    RenderGamePiles()
            );


        }

        private VNode RenderFoundationPiles()
        {
            return Row
                (
                Cards.Foundations.Club.CardPile.Count != 0 ? RenderCard(Cards.Foundations.Club.CardPile.Peek()) : RenderCardback(Styles.CardBlack, "Club"),
                Cards.Foundations.Spade.CardPile.Count != 0 ? RenderCard(Cards.Foundations.Spade.CardPile.Peek()) : RenderCardback(Styles.CardBlack, "Spade"),
                Cards.Foundations.Heart.CardPile.Count != 0 ? RenderCard(Cards.Foundations.Heart.CardPile.Peek()) : RenderCardback(Styles.CardRed, "Heart"),
                Cards.Foundations.Diamond.CardPile.Count != 0 ? RenderCard(Cards.Foundations.Diamond.CardPile.Peek()) : RenderCardback(Styles.CardRed, "Diamond")
                );
        }
        private VNode RenderGamePiles()
        {
            VNode RenderGamePile(CardStack stack)
            {
                if (stack.CardPile.Count != 0)
                    return Col(
                         Fragment(stack.CardPile.Reverse().Take(stack.CardPile.Count - 1).Select(c => RenderOverlappedCard(c))),
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
                Styles.BorderedBoxBlack & card.Color & Styles.W4C & Styles.M2,
                Row(
                    Styles.W4C,
                    Text($"{card.CardSprite}", card.Color & Styles.W2C),
                    Text($"{card.PipSprite}", card.Color & Styles.TextAlignR & Styles.W2C)
                ),
                Text($"{card.CardSprite}", card.Color & Styles.TextAlignC & Styles.W4C & Styles.FontSize3),
                Row(
                    Styles.W4C,
                    Text($"{card.PipSprite}", card.Color & Styles.W2C),
                    Text($"{card.CardSprite}", card.Color & Styles.TextAlignR & Styles.W2C)
                )
            );
            div.OnClick = () => Click(card);

            return div;
        }

        public void Click(Card card)
        {
            if (Selected == null && card.IsFlipped)
                Selected = card;
            else if (Selected == card)
                Selected = null;
            else
            {
                Selected.TryGetStack(Cards).TryMove(card.TryGetStack(Cards), Selected);
                Selected = null;
            }
        }
        private VNode RenderOverlappedCard(Card card)
        {
            if (!card.IsFlipped)
                return Div(
                    Styles.CardBackPartial & Styles.W4C & Styles.M2,
                    Text("XXXXX", Styles.TextAlignC & Styles.W4C)
                );

            var row = Row(
                Styles.W4C & Styles.BorderedBoxPartial & Styles.M2,
                Text($"{card.CardSprite}", card.Color & Styles.W2C),
                Text($"{card.PipSprite}", card.Color & Styles.TextAlignR & Styles.W2C)
            );
            row.OnClick = () => Click(card);
            return row;
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
        private static VNode RenderEmptyCard()
        {
            return Div(Styles.CardEmptyBorderGreen);
        }
    }
}
