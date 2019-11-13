using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class FoundationStack : BaseStack
    {
        public string PipSprite { get; }

        public FoundationStack(PipModel pipModel)
        {
            PipSprite = GetPipSprite(pipModel);
        }

        private string GetPipSprite(PipModel pipModel)
        {
            return pipModel switch
            {
                PipModel.Club => "♣",
                PipModel.Spade => "♠",
                PipModel.Heart => "♥",
                PipModel.Diamond => "♦",
                _ => "0",
            };
        }

        public override bool CanPush(Card card)
        {
            if (card == null)
                return false;
            if (PipSprite != card.PipSprite)
                return false;
            if ((IsEmpty && card.CardValue == CardModel.Ace) || !IsEmpty && (Peek().CardValue == card.CardValue - 1))
                return true;
            return false;
        }

    }
}
