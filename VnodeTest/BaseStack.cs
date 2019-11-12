using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public abstract class BaseStack : Stack<Card>
    {
        public bool IsEmpty => !this.Any();
        public BaseStack()
        {
        }
        public BaseStack(IEnumerable<Card> collection)
            : base(collection)
        {

        }
        public CardStack GetTempStack(Card card)
        {
            CardStack tempStack = new CardStack();
            do
                if (tempStack.IsEmpty || (tempStack.Peek().Color != Peek().Color && tempStack.Peek().CardValue == Peek().CardValue - 1))
                    tempStack.Push(Pop());

            while (tempStack.Peek() != card);
            if (!IsEmpty)
                Peek().IsFlipped = true;

            return tempStack;
        }
        public bool TryPush(BaseStack target, Card card)
        {
            if (CanPush(target, card))
            {
                var tempStack = GetTempStack(card);
                while (tempStack.Count != 0)
                    target.Push(tempStack.Pop());
                return true;
            }
            return false;
        }
        public abstract void ClickEmptyStack(Deck cards, Card selected);
        public abstract bool CanPush(BaseStack target, Card card);
    }
}
