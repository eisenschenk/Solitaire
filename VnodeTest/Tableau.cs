using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Tableau
    {
        public TableauStack TableauSource = new TableauStack();
        public TableauStack TableauGraveyard = new TableauStack();
        public void NextCard()
        {
            if (TableauSource.Count != 0)
            {
                if (!TableauGraveyard.IsEmpty)
                    TableauGraveyard.Peek().IsFlipped = false;
                TableauGraveyard.Push(TableauSource.Pop());
                TableauGraveyard.Peek().IsFlipped = true;
                TableauGraveyard.IsEmpty = false;
            }
            else
            {
                while (TableauGraveyard.Count != 0)
                    TableauSource.Push(TableauGraveyard.Pop());
                TableauGraveyard.IsEmpty = true;
                TableauSource.IsEmpty = false;
            }
        }


        public void ShuffleDeck()
        {

        }
    }
}
