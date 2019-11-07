using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    public class Card
    {
        public Style Color;
        public int Value;
        public int PipID;
        public string PipSprite;


        public Card(int cardDeckIndex)
        {
            //enums
            Value = (cardDeckIndex % 13) + 1;
            PipID = cardDeckIndex / 13;
            if (PipID / 2 == 0)
                Color = Styles.TCblack;
            else
                Color = Styles.TCred;
        }

        public VNode Render()
        {
            return Div
                (
                Styles.BorderedBoxBlack & Styles.W4C & Styles.M2,
                RenderTopPart(),
                RenderBody(),
                RenderBottomPart()
                );
        }
        public VNode RenderTopPart()
        {
            return Row
                (
                Styles.W4C,
                Text($"{Value}", Color & Styles.W2C),
                Text($"{PipID}", Color & Styles.TextAlignR & Styles.W2C)
                );
        }
        private VNode RenderBody()
        {
            return Text($"{Value}", Color & Styles.TextAlignC & Styles.W4C & Styles.FontSize3);
        }
        private VNode RenderBottomPart()
        {
            return Row
                (
                Styles.W4C,
                Text($"{PipID}", Color & Styles.W2C),
                Text($"{Value}", Color & Styles.TextAlignR & Styles.W2C)
                );
        }

        public static VNode RenderCardback(Style color, string title = "Deck")
        {
            return Div
                (
                Styles.BorderedBox & Styles.W4C & Styles.M2& color,
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text(title, Styles.W4C & Styles.TextAlignC),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C),
                Text("XXXXX", Styles.TextAlignC & Styles.W4C)
                );
        }
    }
}
