using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Tableau
    {
        public TableauStack TableauSource { get; }
        public TableauStack TableauGraveyard { get; }
        public int GraveyardTurnedOverCounter { get; set; }
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
                    TableauGraveyard.Peek().IsFaceUp = false;
                TableauGraveyard.Push(TableauSource.Pop());
                TableauGraveyard.Peek().IsFaceUp = true;
            }
            else
            {
                while (TableauGraveyard.Count != 0)
                    TableauSource.Push(TableauGraveyard.Pop());
                GraveyardTurnedOverCounter++;
            }
        }
    }
}
