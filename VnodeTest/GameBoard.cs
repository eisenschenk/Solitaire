using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    //TODO freeze when 2 cards of the same stack are touched after each other
    //TODO Cards dont move to empty cardstack
    //TODO sometimes puts cards in tablea?stack vanishes somewhere
    //TODO putting cards to/from Foudnationstack working?
    //TODO make Style for selected Card/CardStack
    //TODO if clicked somewhere without a Div deselect prior selection maybe
    //TODO implement shuffle


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
                            Cards.Tableau.TableauGraveyard.IsEmpty ? RenderEmptyCard() : RenderCard(Cards.Tableau.TableauGraveyard.Peek())
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
                Cards.Foundations.Club.Count != 0 ? RenderCard(Cards.Foundations.Club.Peek()) : RenderEmptyFoundation(Cards.Foundations.Club, Styles.CardBlack, "Club"),
                Cards.Foundations.Spade.Count != 0 ? RenderCard(Cards.Foundations.Spade.Peek()) : RenderEmptyFoundation(Cards.Foundations.Spade, Styles.CardBlack, "Spade"),
                Cards.Foundations.Heart.Count != 0 ? RenderCard(Cards.Foundations.Heart.Peek()) : RenderEmptyFoundation(Cards.Foundations.Heart, Styles.CardRed, "Heart"),
                Cards.Foundations.Diamond.Count != 0 ? RenderCard(Cards.Foundations.Diamond.Peek()) : RenderEmptyFoundation(Cards.Foundations.Diamond, Styles.CardRed, "Diamond")
                );
        }
        private VNode RenderGamePiles()
        {
            VNode RenderGamePile(CardStack stack)
            {
                if (stack.Count != 0)
                    return Col(
                         Fragment(stack.Reverse().Take(stack.Count - 1).Select(c => RenderOverlappedCard(c))),
                         RenderCard(stack.Peek())
                     );
                else
                {
                    var div = RenderEmptyCard();
                    div.OnClick = () => stack.ClickEmptyStack(Cards, Selected);
                    return div;
                }
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
            div.OnClick = () => ClickCard(card);

            return div;
        }

        public void ClickCard(Card card)
        {
            if (Selected == null && card.IsFlipped)
                Selected = card;
            else if (Selected == card)
                Selected = null;
            else
            {
                Selected.GetStack(Cards).TryPush(card.GetStack(Cards), Selected);
                Selected = null;
            }
        }
        public void ClickEmptyStack(Deck cards, Card selected)
        {
          
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
            row.OnClick = () => ClickCard(card);
            return row;
        }
        private VNode RenderEmptyFoundation(FoundationStack target, Style color, string title = "Deck")
        {
            var div = RenderCardback(color, title);
            div.OnClick = () => target.ClickEmptyStack(Cards, Selected);
            return div;
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
