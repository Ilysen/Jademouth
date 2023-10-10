using XRL.Rules;

namespace XRL.World.Encounters.EncounterObjectBuilders
{
	/// <summary>
	/// Spawns mining gear and a guaranteed Jademouth recoiler.
	/// Note that Atacama also sells everything from <see cref="Tier4Wares"/> - that stock is just provided through a separate part.
	/// </summary>
	public class Ava_Jademouth_AtacamaWares : BaseMerchantWares
	{
		public override void Stock(GameObject GO, string Context = null)
		{
			GO.TakeObject("Spine Fruit", Stat.Random(3, 5), true, Context: Context);
			GO.TakeObject("Pickaxe", Stat.Random(10, 15), true, Context: Context);
			GO.TakeObject("Nanopneumatic Jackhammer", Stat.Random(0, 2), true, Context: Context);
			GO.TakeObject("Miner's Helmet", Stat.Random(1, 2), true, Context: Context);
			GO.TakeObject("Headlamp", Stat.Random(2, 5), true, Context: Context);
			GO.TakeObjectsFromPopulation("Cells", Stat.Random(4, 8), Silent: true, Context: Context);
			GO.TakeObject("Ava_Jademouth_JademouthRecoiler", true, Context: Context);
		}
	}
}
