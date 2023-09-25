using System;
using System.Collections.Generic;
using System.Linq;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World.QuestManagers
{
	/// <summary>
	/// Manager part for the Chaos Theory quest.
	/// Contains logic for the reward and post-quest changes.
	/// </summary>
	[Serializable]
	public class Ilysen_Jademouth_ChaosTheoryManager : QuestManager
	{
		public override void OnQuestComplete()
		{
			/// In order:
			/// 1. Replace all of Bright's mutations
			/// 2. Spawn arc sconces in the workshop
			/// 3. Delete all of the blackout curtains, replacing them with doors
			/// 4. Delete all of the "NO LIGHT PLEASE" signs
			/// It's not ideal to have to use magic strings here, but it's what the base game does, alas
			Zone zone = The.Player.CurrentZone;
			GameObject gameObject = zone.FindObject("Ilysen_Jademouth_Bright");
			if (gameObject != null)
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("Ilysen_Jademouth_Bright_PostQuest");
				gameObject.RequirePart<Description>(false).Short = blueprint.GetPartParameter<string>("Description", "Short", null);
				Mutations muts = gameObject.GetPart("Mutations") as Mutations;
				foreach (BaseMutation mut in muts.MutationList.ToList())
					muts.RemoveMutation(mut);
				muts.AddMutation("Analgesia", 1); // replaces Albino (-2 points)
				muts.AddMutation("Psychometry", 5); // replaces Triple-jointed (3 points)
				muts.AddMutation("ElectricalGeneration", 10); // replaces Multiple Arms (4 points)
				muts.AddMutation("LightManipulation", 5); // replaces Regeneration (4 points)
				gameObject.AddPart(new GivesRep());
				gameObject.SetStringProperty("WaterRitual_Skill", "Tinkering_Tinker2");
				gameObject.pRender.Tile = "Creatures/Bright_2Arms.png"; // GOD I hate this line
			}
			foreach (GameObject go in zone.FindObjects("Ilysen_Jademouth_ChaosTheorySconceSpawner"))
				go.CurrentCell.AddObject("Techlight1", null, null, null, null);
			foreach (GameObject go in zone.FindObjects("Ilysen_Jademouth_BlackoutCurtains").ToList())
			{
				Cell c = go.CurrentCell;
				go.Destroy(null, true, true, null);
				c.AddObject("Door", null, null, null, null);
			}
			foreach (GameObject go in zone.FindObjects("Ilysen_Jademouth_LightSign").ToList())
				go.Destroy(null, true, true, null);
			HandleReward();
			The.Player.RemovePart(this);
		}

		private void HandleReward()
		{
			Dictionary<string, TinkerData> data = new Dictionary<string, TinkerData>();
			foreach (TinkerData td in TinkerData.TinkerRecipes)
			{
				if (td.Type == "Mod" && !TinkerData.RecipeKnown(td))
					data.Add(td.DisplayName, td);
			}
			if (data.Count <= 0)
			{
				Popup.Show("Since you already know every item mod, you muse on the secrets of data disks with Bright.");
				Popup.Show("You gain 10000 XP.");
				The.Player.AwardXP(10000, -1, 0, InfluencedBy: The.Speaker);
				return;
			}
			List<TinkerData> chosenData = new List<TinkerData>();
			for (int i = 0; i < Math.Min(3, data.Count); i++)
			{
				string[] choices = data.Keys.ToArray();
				int choice = Popup.ShowOptionList("", choices, null, 0, $"Choose an item mod to learn, free of charge. You have {3 - i} choice" + (3 - i == 1 ? "" : "s") + " remaining.");
				TinkerData dd = data.GetValue(choices[choice]);
				chosenData.Add(dd);
				data.Remove(choices[choice]);
			}
			foreach (TinkerData disk in chosenData)
			{
				TinkerData.KnownRecipes.Add(disk);
				Popup.Show("Bright teaches you how to mod your items to be {{W|" + disk.DisplayName + "}}.", true, true, true, true);
			}
		}

		public override GameObject GetQuestInfluencer() => GameObject.findByBlueprint("Ilysen_Jademouth_Bright");
	}
}
