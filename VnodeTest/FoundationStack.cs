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
        public override bool CanPush(Card card)
        {
            if (card == null)
                return false;
            if (PipSprite != card.PipSprite)
                return false;
            if ((IsEmpty && card.CardValue == Card.CardModel.Ace) || (Peek().CardValue == card.CardValue - 1))
                return true;
            return false;
        }

    }
}
