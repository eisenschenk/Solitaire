using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardStack : BaseStack
    {
        public bool IsEmpty = true;
        public CardStack(IEnumerable<Card> collection)
            : base(collection) { }

        public CardStack()
        {

        }
        private CardStack GetTempStack(Card card)
        {
            CardStack tempStack = new CardStack();
            var sourcePeak = Peek();
            do
                if (tempStack.IsEmpty || tempStack.Peek().Color != sourcePeak.Color && tempStack.Peek().CardValue == sourcePeak.CardValue - 1)
                    tempStack.Push(Pop());

            while (tempStack.Peek() != card);
            if (Count == 0)
                IsEmpty = true;
            else
                Peek().IsFlipped = true;

            return tempStack;
        }
        public void TryMove(CardStack target, Card sourceCard)
        {
            //var targetCard = target.Peek();

            ////cardstack source
            //if (target is CardStack && !target.IsEmpty && targetCard.Color != sourceCard.Color && targetCard.CardValue == sourceCard.CardValue + 1)
            //{
            //    var tempStack = GetTempStack(sourceCard);
            //    while (tempStack.Count != 0)
            //        target.TryPush(tempStack);
            //}

            //foundation source
            //TODO REDO
            if (target is FoundationStack)
            {
                var fTarget = (FoundationStack)target;
                if (Count == 1 && fTarget.PipSprite == sourceCard.PipSprite && (target.IsEmpty || targetCard.CardValue == sourceCard.CardValue - 1))
                {
                    fTarget.Push(Pop());
                    target.IsEmpty = false;
                }
            }


        }
        private bool CanPush(CardStack target, Card card)
        {
            if (!target.IsEmpty && target.Peek().Color != card.Color && target.Peek().CardValue == card.CardValue + 1)
            {
                var tempStack = GetTempStack(card);
                while (tempStack.Count != 0)
                    target.TryPush(tempStack);
            }
        }


    }
}
