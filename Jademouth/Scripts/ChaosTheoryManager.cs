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
				gameObject.Render.Tile = "Creatures/Bright_2Arms.png"; // GOD I hate this line
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

		public override GameObject GetQuestInfluencer() => GameObject.FindByBlueprint("Ava_Jademouth_Bright");
	}
}
