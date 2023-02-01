using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
    /// <summary>
    /// Jademouth Gravy; preset meal, gives +10-15% max HP and +40-50 Acid Resist.
    /// </summary>
    [Serializable]
    public class JademouthGravy : CookingRecipe
    {
        public JademouthGravy()
        {
            Components.Add(new PreparedCookingRecipieComponentBlueprint("Congealed Skulk", null, 1));
            Components.Add(new PreparedCookingRecipieComponentBlueprint("Worm Jerky", null, 1));
            Components.Add(new PreparedCookingRecipieComponentLiquid("acid", 1));
            Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainArmor_UnitAV", "CookingDomainDarkness_UnitDV" }, null, null)));
        }

        public override string GetDescription() => "+2 AV\n+4 DV";

        public override string GetApplyMessage() => string.Empty;

        public override string GetDisplayName() => "&WJademouth Gravy";
    }
}
