﻿using System;
using System.Linq;
using XRL.Core;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.QuestManagers
{
	/// <summary>
	/// Manager part for the Chaos Theory quest.
	/// Contains the logic for completing some quest steps, as well as all logic for the reward and post-quest changes.
	/// </summary>
	[Serializable]
	public class ChaosTheoryManager : QuestManager
	{
		public override void OnQuestAdded()
		{
			if (The.Player.GetFreeDrams("warmstatic", null, null, null, false) >= 1)
			{
				XRLCore.Core.Game.FinishQuestStep("Chaos Theory", "Find a dram of warm static", -1, true);
				return;
			}
			ThePlayer.AddPart(this, true, false);
			ThePlayer.RegisterPartEvent(this, "Took");
		}

		public override void OnQuestComplete()
		{
			// In order:
			// 1. Alter Bright's mutations
			// 2. Spawn arc sconces in the workshop
			// 3. Delete all of the blackout curtains, replacing them with doors
			// 4. Delete all of the "NO LIGHT PLEASE" signs
			// It's not ideal to have to use magic strings here, but it's what the base game does,so wcyd
			GameObject gameObject = The.Player.CurrentZone.FindObject("Jademouth_Bright");
			if (gameObject != null)
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint("Jademouth_Bright_PostQuest");
				gameObject.RequirePart<Description>(false).Short = blueprint.GetPartParameter<string>("Description", "Short", null);
				Mutations muts = gameObject.GetPart("Mutations") as Mutations;
				foreach (BaseMutation mut in muts.MutationList.ToList())
					muts.RemoveMutation(mut);
				muts.AddMutation("Analgesia", 1); // replaces Albino (-2 points)
				muts.AddMutation("Psychometry", 5); // replaces Triple-jointed (3 points)
				muts.AddMutation("ElectricalGeneration", 10); // replaces Multiple Arms (4 points)
				muts.AddMutation("LightManipulation", 5); // replaces Regeneration (4 points)
				GameObjectFactory.ApplyBuilder(gameObject, "Jademouth_ArtificerDisks");
				gameObject.AddPart(new GivesRep());
				gameObject.SetStringProperty("WaterRitual_Skill", "Tinkering_Tinker2");
			}
			foreach (GameObject go in The.Player.CurrentZone.FindObjects("Jademouth_ChaosTheorySconceSpawner"))
				go.CurrentCell.AddObject("Techlight1", null, null, null, null);
			foreach (GameObject go in The.Player.CurrentZone.FindObjects("Jademouth_BlackoutCurtains").ToList())
			{
				Cell c = go.CurrentCell;
				go.Destroy(null, true, true, null);
                c.AddObject("Door", null, null, null, null);
            }
			foreach (GameObject go in The.Player.CurrentZone.FindObjects("Jademouth_LightSign").ToList())
				go.Destroy(null, true, true, null);
			ThePlayer.RemovePart(this);
		}

		public override GameObject GetQuestInfluencer() => GameObject.findByBlueprint("Jademouth_Bright");

		public override bool WantEvent(int ID, int cascade) => base.WantEvent(ID, cascade) || ID == LiquidMixedEvent.ID;

		public override bool FireEvent(Event E)
		{
			if (E.ID == "Took" && The.Player.GetFreeDrams("warmstatic", null, null, null, false) >= 1)
				XRLCore.Core.Game.FinishQuestStep("Chaos Theory", "Find a dram of warm static", -1, true);
			return base.FireEvent(E);
		}

		public override bool HandleEvent(LiquidMixedEvent E)
		{
			if (The.Player.GetFreeDrams("warmstatic", null, null, null, false) >= 1)
			{
				XRLCore.Core.Game.FinishQuestStep("Chaos Theory", "Find a dram of warm static", -1, true);
				return true;
			}
			return base.HandleEvent(E);
		}
	}
}
