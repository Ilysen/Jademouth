using XRL.Rules;

namespace XRL.World.Encounters.EncounterObjectBuilders
{
	/// <summary>
	/// Identical to <see cref="Tier4Wares"/>, but with additional mining gear and a guaranteed Jademouth recoiler.
	/// </summary>
	public class Jademouth_AtacamaWares : Tier4Wares
	{
		public override void Stock(GameObject GO, string Context = null)
		{
			base.Stock(GO, Context);
			GO.TakeObject("Spine Fruit", Stat.Random(3, 5), true, new int?(0), Context, 0, 0, null, null, null);
			GO.TakeObject("Pickaxe", Stat.Random(10, 15), true, new int?(0), Context, 0, 0, null, null, null);
			GO.TakeObject("Nanopneumatic Jackhammer", Stat.Random(0, 2), true, new int?(0), Context, 0, 0, null, null, null);
			GO.TakeObject("Miner's Helmet", Stat.Random(1, 2), true, new int?(0), Context, 0, 0, null, null, null);
			GO.TakeObject("Headlamp", Stat.Random(2, 5), true, new int?(0), Context, 0, 0, null, null, null);
			GO.TakeObjectsFromPopulation("Cells", Stat.Random(4, 8), null, true, new int?(0), 0, 0, Context, null);
			GO.TakeObject("JademouthRecoiler", true, new int?(0), 0, 0, Context, null, null, null);
		}
	}
}