using System;
using System.Collections.Generic;
using XRL.Core;
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
			Components.Add(new PreparedCookingRecipieComponentBlueprint("Worm Jerky", null, 1));
			Components.Add(new PreparedCookingRecipieComponentLiquid("acid", 1));
			Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainHP_UnitHP", "JademouthGravyMolluskReputation" }, null, null)));
		}

		public override string GetDescription() => "+10-15% max HP\n+300 reputation with mollusks";

		public override string GetApplyMessage() => string.Empty;

		public override string GetDisplayName() => "&WJademouth Gravy";
	}
}

namespace XRL.World.Effects
{
	/// <summary>
	/// Used in Jademouth Gravy. Gives +300 mollusk reputation.
	/// </summary>
	[Serializable]
	public class JademouthGravyMolluskReputation : ProceduralCookingEffectUnit
	{
		public override string GetDescription() => "+300 reputation with mollusks";

		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer())
			{
				XRLCore.Core.Game.PlayerReputation.modify("Mollusks", 300, "Cooking", null, null, true, true, 0);
				Applied = true;
			}
		}

		public override void Remove(GameObject Object, Effect parent)
		{
			if (Applied)
				XRLCore.Core.Game.PlayerReputation.modify("Mollusks", -300, "Cooking", null, null, true, true, 0);
		}

		private bool Applied;
	}
}
