using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class CardStack : BaseStack
    {

        public CardStack(IEnumerable<Card> collection)
            : base(collection) { }

        public CardStack()
        {

        }
        public override void ClickEmptyStack(Deck cards, Card selected)
        {
                TryPush(selected, selected.GetStack(cards));
        }
        public override bool CanPush(Card card)
        {
            if (card == null)
                return false;
            if (IsEmpty && card.CardValue == Card.CardModel.King)
                return true;
            if (!IsEmpty && Peek().Color != card.Color && Peek().CardValue == card.CardValue + 1)
                return true;

            return false;
        }
    }
}
