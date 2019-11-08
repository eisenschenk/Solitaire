using ACL.UI.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACL.UI.React.DOM;

namespace Solitaire
{
    class DragController
    {
        public class Marble
        {
            public string Name;
            public bool Selected;
            public Marble(string name)
            {
                Name = name;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        public class Bucket
        {
            public string Name;
            public Bucket(string name, List<Marble> items)
            {
                Name = name;
                Items = items;
            }
            public List<Marble> Items = new List<Marble>();
            public IEnumerable<Marble> SelectedItems => Items?.Where(i => i.Selected);
            public override string ToString()
            {
                return Name;
            }
        }

        public Bucket Eimer1 = new Bucket("Eimer 1", Enumerable.Range('A', 'V' - 'A' + 1).Select(c => new Marble(((char)c).ToString())).ToList());
        public Bucket Eimer2 = new Bucket("Eimer 2", new List<Marble>());
        public Bucket Eimer3 = new Bucket("Eimer 3", new List<Marble>());
        DragState<Marble, Bucket> _MarbleDragState = new DragState<Marble, Bucket>();
        Action<DragContext, DragAction, VNode> MarbleDragAction => MarbleDrag;

        private void MarbleDrag(DragContext context, DragAction dragAction, VNode node)
        {
            _DragContext = context;
            _MarbleDragState.Update(context);

            System.Diagnostics.Debug.WriteLine("DRAG " + dragAction + " src: " + _MarbleDragState.Source + " tar:" + _MarbleDragState.Target);

            if (dragAction == DragAction.Drop)
            {
                if (_MarbleDragState.Complete)
                {
                    List<Bucket> buckets = new List<Bucket>() { this.Eimer1, this.Eimer2, this.Eimer3 };
                    IEnumerable<Marble> selectedMarbles = buckets.SelectMany(b => b.SelectedItems);
                    IEnumerable<Marble> drobMarbles = new Marble[] { _MarbleDragState.Source };
                    List<Marble> changingMarbles = drobMarbles.Concat(selectedMarbles).Distinct().Where(m => m != null).ToList();

                    foreach (Bucket bucket in buckets)
                    {
                        foreach (Marble marble in changingMarbles)
                            bucket.Items.Remove(marble);
                    }

                    Bucket target = _MarbleDragState.Target;
                    target.Items.AddRange(changingMarbles);
                }
            }
        }

        public VNode Test3()
        {

            System.Diagnostics.Debug.WriteLine("RENDER");
            VNode container = Div(Styles.Relative);

            VNode draglayer = Div().WithCustomStyle("background-color:red; opacity: 0.5;");
            draglayer.Drag = new DragInfo(DragMode.Proxy);
            draglayer.OnDrag = MarbleDragAction;
            draglayer.Rect = new Rect(
                left: 0,
                top: 0,
                width: 0,
                height: 0, unit: "rem");

            List<VNode> binList = new List<VNode>();
            binList.Add(RenderBucket(Eimer1, Styles.Absolute, 0, draglayer));
            binList.Add(RenderBucket(Eimer2, Styles.Absolute, 12, draglayer));
            binList.Add(RenderBucket(Eimer3, Styles.Absolute, 24, draglayer));

            container.Rect = new Rect(
                    left: 0,
                    top: 0,
                    width: binList.Last()?.Rect?.Right ?? 0,
                    height: binList.Last()?.Rect?.Bottom ?? 0);

            if (draglayer.Children?.Any() ?? false)
            {
                binList.Add(draglayer);
            }
            container.AppendChildren(binList);
            return container;

        }

        private void ChangeSelection(Marble item)
        {
            item.Selected = !item.Selected;
        }

        /// <summary>
        /// Rendert ein den Bucket mit einen Marbles.
        /// Zusätzlich wird die aktive Source in den DragLayer (Proxy) gepackt.
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="style"></param>
        /// <param name="left"></param>
        /// <param name="draglayer"></param>
        /// <returns></returns>
        VNode RenderBucket(Bucket bucket, Style style, decimal left, VNode draglayer)
        {
            decimal binwidth = 10;
            decimal binheight = 10;

            decimal childpad = 0.1m;
            decimal childleft = 0;
            decimal childtop = 0;
            decimal childwidth = 2;
            decimal childheight = 2;

            decimal selectpad = childpad / 2;
            decimal selectwidth = childwidth / 2;
            decimal selectheight = childheight / 2;

            List<Marble> selected = bucket.Items.Where(m => m.Selected).ToList();
            selected.Remove(_MarbleDragState.Source);

            int selectCount = draglayer.Children?.Where(c => c.Rect != null).Count(c => c.Rect.Top == childpad + childheight) ?? 0;

            List<VNode> children = new List<VNode>();
            foreach (Marble marble in bucket.Items)
            {
                VNode child = Text(marble?.Name, Styles.Absolute & (marble.Selected ? Styles.Selected : null)).WithCustomStyle("background-color: yellow;");
                child.Key = marble;
                child.OnClick = () => ChangeSelection(marble);
                Rect rect = new Rect(left: childleft + childpad, top: childtop + childpad, width: childwidth - 2 * childpad, height: childheight - 2 * childpad, unit: "rem");

                bool isProxy = false;

                // DragMode als Source oder als Proxy setzen
                if (_MarbleDragState.Source == null)
                {
                    child.Drag = new DragInfo(DragMode.Source);
                    child.OnDrag = MarbleDragAction;
                }
                else if (_MarbleDragState.Source == marble)
                {
                    isProxy = true;
                    draglayer.AppendChild(child);
                    child.Rect = new Rect(left: childpad, top: childpad, width: childwidth - 2 * childpad, height: childheight - 2 * childpad, unit: "rem");
                    System.Diagnostics.Debug.WriteLine("A " + child.Rect + "    " + "X" + "    " + child.Key);
                }
                // Wenn gerade gedraggt wird und die aktuelle Marble selected ist => mit verschieben
                else if (marble.Selected)
                {
                    VNode dragcopy = Text(child.Text, child.Style);
                    dragcopy.Key = child.Key;

                    draglayer.AppendChild(dragcopy);
                    dragcopy.Rect = new Rect(left: childpad + (selectCount++ * selectwidth), top: childpad + childheight, width: selectwidth - 2 * selectpad, height: selectheight - 2 * selectpad, unit: "rem");
                    dragcopy.Animation = new NodeAnimation(rect);
                    System.Diagnostics.Debug.WriteLine("S " + child.Rect + "    " + selectCount + "    " + child.Key);
                }

                // Wenn es sich um ein Proxy-Child handelt wandert er in den ProxyLayer
                // ansonsten wird er als Child im Bin eingefügt.
                if (!isProxy)
                {
                    child.Rect = rect;
                    children.Add(child);
                }

                // ChildLeft und ChildTop neu ausrechnen für die Verteilung im Bin
                childleft += childwidth;
                if ((childleft + childwidth) > binwidth)
                {
                    childleft = 0;
                    childtop += childheight;
                }
            }

            VNode bin = Div(style, children).WithCustomStyle("background-color: blue;");
            bin.Rect = new Rect(
                left: left,
                top: 0,
                width: binwidth,
                height: binheight, unit: "rem");
            bin.Key = bucket;

            // Der Bin muss nur als Ziel markiert werden wenn überhaupt etwas gedragt ist
            if (_MarbleDragState.Active)
            {
                bin.Drag = new DragInfo(DragMode.Target);
                bin.OnDrag = MarbleDragAction;
            }

            return bin;
        }

        /**
		 .test3
{
    position: relative;
}
.test3draglayer
{
    background-color:none;
    background-color:red;
    opacity: 0.5;
}
.eimer1
{
	position: absolute;
    background-color:#646464;

     &.dragover 
	{
		background-color:#C37505;
	}  
}
.eimer2
{
	position: absolute;
    background-color:#969696;	
    
    &.dragover 
	{		
        background-color:#FAA832;
	} 
}
.eimer3
{
	position: absolute;
    background-color:#C8C8C8;
    
    &.dragover 
	{
        background-color:#FCD194;
	} 
}
.marble
{
    position: absolute;
    background-color:#F57C00;       
    opacity: 0.8;

     text-align: center;
    vertical-align: middle;
    @include border-radius(50%);

    &.selected
    {
        background-color:#00BCD4;        
    }

    &.animation
    {
        opacity: 0.5; 
        // transition: all 2.5s ease-out;        
        transition-property: opacity, top, left, width, height;
        transition-duration: 1.5s;
        transition-timing-function: ease-in;   
    }
}
		 */
        DragContext _DragContext;
    }
    /// <summary>
    /// ein dragstate der aus einem context alle sources und targets vom typ T extrahiert
    /// </summary>
    public class DragState<T> : DragState<T, T>
        where T : class
    {
    }

    /// <summary>
    /// ein dragstate der aus einem context alle sources vom typ TSource und alle targets vom typ TTarget extrahiert
    /// </summary>
    public class DragState<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        public DragContext Context;
        public TSource Source;
        public TTarget Target;

        public List<TTarget> Targets;

        /// <summary>
        /// dragstate ist NUR active, wenn mindestens eine source da ist
        /// ansonsten würde das active auch angetriggert, wenn der dragstate auf einen typ als target prüft der auch in anderen dragstates verwendet wird
        /// </summary>
        public bool Active => Source != null;

        /// <summary>
        /// der dragstate ist vollständig, es sind source und targets vorhanden, so dass eine drop-operation ausgeführt werden kann
        /// </summary>
        public bool Complete => Source != null && Target != null;

        public void Update(DragContext context)
        {
            Context = context;
            Source = context?.SourcesOfType<TSource>().FirstOrDefault();
            // nur wenn eine source da ist können auch targets identifiziert werden
            Targets = Source != null ? context?.TargetsOfType<TTarget>().ToList() : null;
            Target = Targets?.FirstOrDefault();
        }
    }

}
