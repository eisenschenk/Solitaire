using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardStack
    {
        public Stack<Card> CardPile = new Stack<Card>();
        private Stack<Card> TempPile = new Stack<Card>();
        public bool IsEmpty = true;
        public CardStack(IEnumerable<Card> collection)
        {
            CardPile = new Stack<Card>(collection);
        }
        public CardStack()
        {

        }
        public void TryMove(CardStack target)
        {
            var targetCard = target.CardPile.Peek();
            var sourceCard = CardPile.Peek();
            var fTarget = (FoundationStack)target;
            if (target is CardStack && targetCard.PipValue == sourceCard.PipValue && targetCard.CardValue == sourceCard.CardValue + 1)
            {
                foreach (Card card in CardPile)
                    TempPile.Push(CardPile.Pop());
                foreach (Card card in TempPile)
                    target.CardPile.Push(TempPile.Pop());
                target.IsEmpty = false;
                if (CardPile.Count == 0)
                    IsEmpty = true;
            }
            if (target is FoundationStack)
            {
                if (CardPile.Count == 1 && fTarget.PipSprite == sourceCard.PipSprite && (target.IsEmpty || targetCard.CardValue == sourceCard.CardValue - 1))
                {
                    target.CardPile.Push(CardPile.Pop());
                    target.IsEmpty = false;
                }
            }
            if (CardPile.Count == 0)
                IsEmpty = true;
        }


    }
}
