using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World.Encounters.EncounterObjectBuilders
{
	/// <summary>
	/// Used as a reward for Chaos Theory.
	/// Generates a data disk for every item mod, modified to be twice as expensive.
	/// </summary>
	public class Jademouth_ArtificerDisks
	{
		public bool BuildObject(GameObject GO, string Context = null)
		{
			foreach (TinkerData td in TinkerData.TinkerRecipes)
			{
				if (td.Type == "Mod")
				{
					GameObject disk = TinkerData.createDataDisk(td);
					Commerce c = disk.GetPart("Commerce") as Commerce;
					c.Value *= 2; // Markup so that we can at least pretend it's balanced
					GO.TakeObject(disk, true, new int?(0), null, null);
				}
			}
			return true;
		}
	}
}