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

        [NonSerialized]
        private bool shouldRecacheJade = true;

        [NonSerialized]
        private bool hasJade = false;
    }
}
