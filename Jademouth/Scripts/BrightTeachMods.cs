using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Language;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Conversations.Parts
{
	/// <summary>
	/// Script part for Bright's conversation that handles the reward for the Chaos Theory quest.
	/// </summary>
	public class Ava_Jademouth_BrightTeachMods : IConversationPart
	{
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID;
		}

		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[Learn 3 item mods]}}";
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(EnterElementEvent E)
		{
			HandleReward();
			return base.HandleEvent(E);
		}

		/// <summary>
		/// Handles the reward of Chaos Theory by allowing the player to choose three item mods they don't know, free of charge and ignoring skill requirements.
		/// If the player already has all mods, they receive a bunch of XP instead. If they're only missing three or less, then those ones are chosen automatically.
		/// </summary>
		private void HandleReward()
		{
			SortedList<string, TinkerData> sortedList = new SortedList<string, TinkerData>();
			foreach (TinkerData td in TinkerData.TinkerRecipes.Where(x => x.Type == "Mod" && !TinkerData.RecipeKnown(x)))
				sortedList.Add(td.DisplayName, td);
			ChooseReward:
			List<int> chosenIndexes = new List<int>();
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
			{
				var pickedIndices = Popup.PickSeveral("Choose up to three item mods to learn, free of charge.\nProceed with no selections to cancel.", Options: sortedList.Keys.ToArray(), Context: The.Speaker, Amount: 3);
				foreach (var (Selected, Amount) in pickedIndices)
					chosenIndexes.Add(Selected);
			}

			if (chosenIndexes.Count == 0)
			{
				if (Popup.ShowYesNo("Cancel learning? You will be able to return later.") == DialogResult.Yes)
					return;
				goto ChooseReward;
			}
			else if (chosenIndexes.Count < Math.Min(3, sortedList.Count))
			{
				if (Popup.ShowYesNo($"You can choose {Math.Min(3, sortedList.Count)} mods to learn, but you've only selected {chosenIndexes.Count}. Really continue?") != DialogResult.Yes)
					goto ChooseReward;
			}

			var chosenEntries = new List<TinkerData>();
			foreach (int i in chosenIndexes)
				chosenEntries.Add(sortedList.ElementAt(i).Value);

			if (Popup.ShowYesNo($"Learn the {Grammar.MakeAndList(chosenEntries.Select(x => "{{W|" + x.DisplayName + "}}").ToList())} item mods?") != DialogResult.Yes)
				goto ChooseReward;

			foreach (TinkerData td in chosenEntries)
			{
				TinkerData.KnownRecipes.Add(td);
				The.Player.PlayWorldOrUISound("Sounds/Interact/sfx_interact_dataDisk_learn");
				Popup.Show("Bright teaches you how to mod items with the {{W|" + td.DisplayName + "}} mod.");
			}
			The.Player.RemoveIntProperty("Ava_Jademouth_BrightRewardingPending");
		}
	}
}
