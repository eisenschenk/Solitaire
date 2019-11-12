using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class FoundationStack : BaseStack
    {
        public string PipSprite;

        public FoundationStack(Foundations.PipModel pipModel)
        {
            PipSprite = GetPipSprite(pipModel);
        }


        private string GetPipSprite(Foundations.PipModel pipModel)
        {
            switch (pipModel)
            {
                case Foundations.PipModel.Club: return "♣";
                case Foundations.PipModel.Spade: return "♠";
                case Foundations.PipModel.Heart: return "♥";
                case Foundations.PipModel.Diamond: return "♦";
                default: return "0";
            }
        }
        public override void ClickEmptyStack(Deck cards, Card selected)
        {
            if (selected != null && selected.CardValue == Card.CardModel.Ace)
                selected.GetStack(cards).TryPush(this, selected);
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
            return false;
        }

    }
}
