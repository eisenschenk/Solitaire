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
        private readonly Deck Cards = new Deck();
        private Card Selected;
        private int Score => GetScore();

        public GameBoard()
        {
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
                    RenderFoundationPiles(),
                    RenderScore()
                ),
                RenderGamePiles(),
                RenderWin()
            );
        }

        private VNode RenderWin()
        {
            var hasClosedCards = Cards.GamePiles
                .Where(x => x.Any(c => !c.IsFaceUp))
                .Any();
            if (hasClosedCards)
                return null;
            return Div(
                Styles.TCgreen & Styles.WinBox & Styles.AlignItemCenter & Styles.MT2,
                Text($"You Won!", Styles.FontSize3),
                Text($"Score: {Score}", Styles.FontSize3)
                );
        }

        private VNode RenderFoundationPiles()
        {
            VNode renderFoundation(FoundationStack foundation, Style color, string pip) =>
                foundation.Count != 0
                    ? RenderCard(foundation.Peek())
                    : RenderEmptyFoundation(foundation, color, pip);

            return Row(
                renderFoundation(Cards.Foundations.Club, Styles.CardBlack, "Club"),
                renderFoundation(Cards.Foundations.Spade, Styles.CardBlack, "Spade"),
                renderFoundation(Cards.Foundations.Heart, Styles.CardRed, "Heart"),
                renderFoundation(Cards.Foundations.Diamond, Styles.CardRed, "Diamond")
            );
        }

        //TODO final:52*10-cycles-time
        private int GetScore()
        {
            //score for all cards put to Foundations
            var score = Cards.Foundations.Club.Count + Cards.Foundations.Spade.Count + Cards.Foundations.Heart.Count + Cards.Foundations.Diamond.Count;
            //factor per card
            score *= 10;
            // point reduction per full cycle of cards shown from Tableau
            score -= Cards.Tableau.GraveyardTurnedOverCounter * 20;
            return score;
        }

        public VNode RenderScore()
        {
            return Row(
                Styles.BorderedBoxPurple & Styles.Ml6,
                Text($"Score:", Styles.W3C),
                Text(Score.ToString(), Styles.TextAlignR & Styles.W3C)
            );
        }

        private VNode RenderGamePiles()
        {
            VNode RenderGamePile(CardStack stack)
            {
                if (stack.Count != 0)
                    return Col(
                        Fragment(stack.Reverse().Take(stack.Count - 1).Select(RenderOverlappedCard)),
                        RenderCard(stack.Peek())
                    );
                var div = RenderEmptyCard();
                div.OnClick = () => { stack.ClickEmptyStack(Cards, Selected); Selected = null; };
                return div;
            }
            return Row(Cards.GamePiles.Select(p => RenderGamePile(p)));
        }

        private VNode RenderCard(Card card)
        {
            var boxStyle = card == Selected ? Styles.BorderedBoxPurple : Styles.BorderedBoxBlack;

            if (!card.IsFaceUp)
                return Div(() => card.IsFaceUp = true, RenderCardback(Styles.CardGreen, "Click me!"));

            return Div(
                card.Color & Styles.W4C & Styles.M2 & boxStyle,
                () => ClickCard(card),
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
        }

        public void ClickCard(Card card)
        {
            if (Selected == null)
                Selected = card;
            else if (Selected == card)
                Selected = null;
            else
            {
                card.GetStack(Cards).TryPush(Selected, Selected.GetStack(Cards));
                Selected = null;
            }
        }

        private VNode RenderOverlappedCard(Card card)
        {
            Style cardStyle = card == Selected ? Styles.BorderedBoxPartialSelected : Styles.BorderedBoxPartial;

            if (!card.IsFaceUp)
                return Div(
                    Styles.CardBackPartial & Styles.W4C & Styles.M2,
                    Text("XXXXX", Styles.TextAlignC & Styles.W4C)
                );

            return Row(
                Styles.W4C & cardStyle & Styles.M2,
                () => ClickCard(card),
                Text($"{card.CardSprite}", card.Color & Styles.W2C),
                Text($"{card.PipSprite}", card.Color & Styles.TextAlignR & Styles.W2C)
            );
        }

        private VNode RenderEmptyFoundation(FoundationStack target, Style color, string title = "Deck")
        {
            var div = RenderCardback(color, title);
            div.OnClick = () => { target.ClickEmptyStack(Cards, Selected); Selected = null; };
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
