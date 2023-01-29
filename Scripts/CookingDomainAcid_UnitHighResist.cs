using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	/// <summary>
	/// Used in Jademouth Gravy. Gives 40-50 acid resist.
	/// Serializable classes sadly can't be inherited, so we have to remake the whole functionality.
	/// </summary>
	[Serializable]
	public class CookingDomainAcid_UnitHighResist : ProceduralCookingEffectUnit
	{
		public override string GetDescription() => "+" + Bonus.ToString() + " Acid Resist";

		public override string GetTemplatedDescription() => "+40-50 Acid Resist";

		public override void Init(GameObject target) => Bonus = Stat.Random(40, 50);

		public override void Apply(GameObject Object, Effect parent) => Object.Statistics["AcidResistance"].Bonus += Bonus;

		public override void Remove(GameObject Object, Effect parent) => Object.Statistics["AcidResistance"].Bonus -= Bonus;

		public int Bonus;
	}
}
