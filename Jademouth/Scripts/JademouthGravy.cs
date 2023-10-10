using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.Effects;

namespace XRL.World.Skills.Cooking
{
	/// <summary>
	/// Code for Jademouth Gravy, the preset meal in Jademouth. Current effects are +10-15% max HP and +300 reputation with mollusks.
	/// </summary>
	[Serializable]
	public class Ava_Jademouth_JademouthGravy : CookingRecipe
	{
		public Ava_Jademouth_JademouthGravy()
		{
			Components.Add(new PreparedCookingRecipieComponentBlueprint("Worm Jerky", null, 1));
			Components.Add(new PreparedCookingRecipieComponentLiquid("acid", 1));
			Effects.Add(new CookingRecipeResultProceduralEffect(ProceduralCookingEffect.CreateSpecific(new List<string> { nameof(CookingDomainHP_UnitHP), nameof(Ava_Jademouth_CookingEffectMolluskRep) })));
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
	public class Ava_Jademouth_CookingEffectMolluskRep : ProceduralCookingEffectUnit
	{
		public override string GetDescription() => "+300 reputation with mollusks";

		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer())
			{
				XRLCore.Core.Game.PlayerReputation.modify("Mollusks", 300, "Cooking", Silent: true, Transient: true);
				Applied = true;
			}
		}

		public override void Remove(GameObject Object, Effect parent)
		{
			if (Applied)
				XRLCore.Core.Game.PlayerReputation.modify("Mollusks", -300, "Cooking", Silent: true, Transient: true);
		}

		private bool Applied;
	}
}
