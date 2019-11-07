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
        //not working propperly
        private void DealCards()
        {
            for (int index = 0; index < 7; index++)
            {
                CardDeck.GamePiles[index].Add(CardDeck.Deck[0]);
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
                        Div(() => NextCard(), Card.RenderCardback("green")),
                        CardDeck.Deck[CurrentCardIndex].Render()
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
                CardDeck.FoundationpileClub.Count != 0 ? CardDeck.FoundationpileClub[CardDeck.FoundationpileClub.Count - 1].Render() : Card.RenderCardback("black", "Club"),
                CardDeck.FoundationpileSpade.Count != 0 ? CardDeck.FoundationpileSpade[CardDeck.FoundationpileSpade.Count - 1].Render() : Card.RenderCardback("black", "Spade"),
                CardDeck.FoundationpileHeart.Count != 0 ? CardDeck.FoundationpileHeart[CardDeck.FoundationpileHeart.Count - 1].Render() : Card.RenderCardback("red", "Heart"),
                CardDeck.FoundationpileDiamonds.Count != 0 ? CardDeck.FoundationpileDiamonds[CardDeck.FoundationpileDiamonds.Count - 1].Render() : Card.RenderCardback("red", "Diamonds")
                );
        }
        private VNode RenderGamePiles()
        {
            return Div();






        }
    }
}
