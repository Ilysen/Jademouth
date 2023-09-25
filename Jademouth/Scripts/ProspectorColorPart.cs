using System;
using System.Linq;

namespace XRL.World.Parts
{
    /// <summary>
    /// This logic makes jade prospectors have a bright green detail color when they have jade to sell.
    /// Jade is sold by individual prospectors rather than a main merchant, so this makes shopping easier for the player to do.
    /// </summary>
    [Serializable]
    public class Ilysen_Jademouth_ProspectorColorPart : IPart
    {
        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, "EncumbranceChanged");
            base.Register(Object);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "EncumbranceChanged")
                shouldRecacheJade = true;
            return base.FireEvent(E);
        }

        public override bool Render(RenderEvent E)
        {
            if (shouldRecacheJade)
            {
                hasJade = ParentObject.Inventory.GetObjects().Where(x => x.Blueprint.ToLower().Contains("jade")).Count() > 0;
                shouldRecacheJade = false;
            }
            if (hasJade)
                E.ApplyColors(null, "G", 0, 81);
            return base.Render(E);
        }

        /// <summary>
        /// If this is <c>true</c> when the game renders a frame, then it will search the parent object's inventory for jade and save the result to <see cref="hasJade"/>,
        /// then set itself to <c>false</c>.
        /// <br/><br/>
        /// This is set to <c>true</c> whenever an event of ID <c>"EncumbranceChanged"</c> is fired on the parent object.
        /// </summary>
        [NonSerialized]
        private bool shouldRecacheJade = true;

        /// <summary>
        /// Whether or not the parent object's inventory has at least one item with <c>jade</c> in its name.
        /// </summary>
        [NonSerialized]
        private bool hasJade = false;
    }
}
