using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Tableau
    {
        public TableauStack TableauSource;
        public TableauStack TableauGraveyard;
        public Tableau(IEnumerable<Card> collection)
        {
            TableauSource = new TableauStack(collection);
            TableauGraveyard = new TableauStack();

        }

        public void NextCard()
        {
            if (TableauSource.Count != 0)
            {
                if (!TableauGraveyard.IsEmpty)
                    TableauGraveyard.Peek().IsFlipped = false;
                TableauGraveyard.Push(TableauSource.Pop());
                TableauGraveyard.Peek().IsFlipped = true;
            }
            else
            {
                while (TableauGraveyard.Count != 0)
                    TableauSource.Push(TableauGraveyard.Pop());
            }
        }



    }
}
