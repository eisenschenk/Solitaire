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

        public BaseStack() { }

        public BaseStack(IEnumerable<Card> collection)
            : base(collection) { }

        public CardStack GetTempStack(Card card)
        {
            CardStack tempStack = new CardStack();
            do tempStack.Push(Pop());
            while (tempStack.Peek() != card);

            return tempStack;
        }

        public bool TryPush(Card sourceCard, BaseStack sourceStack)
        {
            if (CanPush(sourceCard))
            {
                var tempStack = sourceStack.GetTempStack(sourceCard);
                while (tempStack.Count != 0)
                    Push(tempStack.Pop());
                return true;
            }
            return false;
        }

        public void ClickEmptyStack(Deck cards, Card selected)
        {
            TryPush(selected, cards.GetStack(selected));
        }

        public abstract bool CanPush(Card card);
    }
}
