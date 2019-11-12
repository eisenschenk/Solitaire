using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class TableauStack : BaseStack
    {
        public TableauStack(IEnumerable<Card> collection)
            : base(collection)
        {

        }
        public TableauStack()
        {

        }
        public override void ClickEmptyStack(Deck cards, Card selected)
        {

        }
        public override bool CanPush(BaseStack target, Card card)
        {
            if (target is CardStack)
            {
                if (target.IsEmpty && card.CardValue == Card.CardModel.King)
                    return true;
                if (!target.IsEmpty && target.Peek().Color != card.Color && target.Peek().CardValue == card.CardValue + 1)
                    return true;
            }
            if (target is FoundationStack)
            {
                var fTarget = (FoundationStack)target;
                if (fTarget.PipSprite != card.PipSprite)
                    return false;
                if ((target.IsEmpty && card.CardValue == Card.CardModel.Ace) || (fTarget.Peek().CardValue == card.CardValue - 1))
                    return true;
            }
            return false;
        }




    }
}
