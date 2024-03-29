﻿using System;
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
	public class Ava_Jademouth_ChaosTheoryManager : QuestManager
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
			GameObject gameObject = zone.FindObject("Ava_Jademouth_Bright");
			if (gameObject != null)
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("Ava_Jademouth_Bright_PostQuest");
				gameObject.RequirePart<Description>(false).Short = blueprint.GetPartParameter<string>("Description", "Short", null);
				if (gameObject.TryGetPart(out Mutations m))
				{
					foreach (BaseMutation mut in m.MutationList.ToList())
						m.RemoveMutation(mut);
					m.AddMutation("Analgesia", 1); // replaces Albino (-2 points)
					m.AddMutation("Psychometry", 5); // replaces Triple-jointed (3 points)
					m.AddMutation("ElectricalGeneration", 10); // replaces Multiple Arms (4 points)
					m.AddMutation("LightManipulation", 5); // replaces Regeneration (4 points)
				}
				gameObject.AddPart(new GivesRep());
				gameObject.SetStringProperty("WaterRitual_Skill", "Tinkering_Tinker2");
				gameObject.pRender.Tile = "Creatures/Bright_2Arms.png"; // GOD I hate this line
			}
			foreach (GameObject go in zone.FindObjects("Ava_Jademouth_ChaosTheorySconceSpawner"))
			{
				go.CurrentCell.AddObject("Techlight1");
				go.Obliterate(Silent: true);
			}
			foreach (GameObject go in zone.FindObjects("Ava_Jademouth_BlackoutCurtains"))
			{
				Cell c = go.CurrentCell;
				go.Obliterate(Silent: true);
				c.AddObject("Door");
			}
			foreach (GameObject go in zone.FindObjects("Ava_Jademouth_LightSign"))
				go.Obliterate(Silent: true);
			The.Player.RemovePart(this);
			The.Player.SetIntProperty("Ava_Jademouth_BrightRewardingPending", 1);
		}

		/// <summary>
		/// Handles the reward of Chaos Theory by allowing the player to choose three item mods they don't know, free of charge and ignoring skill requirements.
		/// If the player already has all mods, they receive a bunch of XP instead. If they're only missing three or less, then those ones are chosen automatically.
		/// </summary>
		public static void HandleReward()
		{
			SortedList<string, TinkerData> sortedList = new SortedList<string, TinkerData>();
			foreach (TinkerData td in TinkerData.TinkerRecipes.Where(x => x.Type == "Mod" && !TinkerData.RecipeKnown(x)))
				sortedList.Add(td.DisplayName, td);
			List<int> chosenIndexes = new List<int>();
		ChooseReward:
			if (sortedList.Count == 0)
			{
				Popup.Show("Since you already know every item mod, you muse on the secrets of data disks with Bright.");
				Popup.Show("You gain 10000 XP.");
				The.Player.AwardXP(10000, -1, 0, InfluencedBy: The.Speaker);
				return;
			}
			else if (sortedList.Count <= 3)
			{
				for (int i = 0; i < sortedList.Count; i++)
					chosenIndexes.Add(i);
			}
			else
				chosenIndexes = Popup.PickSeveral("Choose up to three item mods to learn, free of charge.", sortedList.Keys.ToArray(), Amount: 3);

			if (chosenIndexes.Count == 0)
			{
				if (Popup.ShowYesNo("Cancel learning? You will be able to return later.") == DialogResult.Yes)
					return;
				goto ChooseReward;
			}
			else if (chosenIndexes.Count < Math.Min(3, sortedList.Count))
				if (Popup.ShowYesNo($"You can choose {Math.Min(3, sortedList.Count)} mods to learn, but you've only selected {chosenIndexes.Count}. Really continue?") != DialogResult.Yes)
					goto ChooseReward;

			foreach (int i in chosenIndexes)
			{
				TinkerData td = sortedList.ElementAt(i).Value;
				TinkerData.KnownRecipes.Add(td);
				Popup.Show("Bright teaches you how to mod your items to be {{W|" + td.DisplayName + "}}.");
			}
			The.Player.RemoveIntProperty("Ava_Jademouth_BrightRewardingPending");
		}

		public override GameObject GetQuestInfluencer() => GameObject.findByBlueprint("Ava_Jademouth_Bright");
	}
}
