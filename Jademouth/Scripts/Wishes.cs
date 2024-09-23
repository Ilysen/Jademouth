using XRL;
using XRL.Messages;
using XRL.Wish;
using XRL.World;

namespace Ava.Jademouth.Scripts
{
	[HasWishCommand]
	public class WishHandler
	{
		[WishCommand(Command = "go2jademouth")]
		public static void GoToJademouth()
		{
			The.Player.ZoneTeleport("JoppaWorld.42.6.1.1.10", 52, 15);
		}

		[WishCommand(Command = "chaostheorytest")]
		public static void ChaosTheoryTest()
		{
			GoToJademouth();
			The.Game.CompleteQuest("Ava_Jademouth_ChaosTheory");
			GameObject bright = The.ActiveZone.FindObject("Ava_Jademouth_Bright");
			if (bright != null)
				The.Player.DirectMoveTo(bright.CurrentCell.GetEmptyAdjacentCells()[0]);
		}

		[WishCommand(Command = "jademouthpeace")]
		public static void JademouthPeace()
		{
			bool atacama = false, bright = false, fizz = false, cat = false;
			if (The.Game.GetStringGameState("Ava_Jademouth_AtacamaState").Contains("Dead"))
			{
				The.Game.RemoveStringGameState("Ava_Jademouth_AtacamaState");
				atacama = true;
			}
			if (The.Game.GetStringGameState("Ava_Jademouth_BrightState").Contains("Dead"))
			{
				The.Game.RemoveStringGameState("Ava_Jademouth_BrightState");
				bright = true;
			}
			if (The.Game.GetStringGameState("Ava_Jademouth_FizzState").Contains("Dead"))
			{
				The.Game.RemoveStringGameState("Ava_Jademouth_FizzState");
				fizz = true;
			}
			if (The.Game.GetStringGameState("Ava_Jademouth_CatState").Contains("Dead"))
			{
				The.Game.RemoveStringGameState("Ava_Jademouth_CatState");
				cat = true;
			}
			if (atacama)
				MessageQueue.AddPlayerMessage($"Atacama's death tracker reset.");
			if (bright)
				MessageQueue.AddPlayerMessage($"Bright's death tracker reset.");
			if (fizz)
				MessageQueue.AddPlayerMessage($"Fizz's death tracker reset.");
			if (cat)
				MessageQueue.AddPlayerMessage($"Cat's death tracker reset.");
			MessageQueue.AddPlayerMessage("Jademouth NPC death trackers reset. If any were changed, you should see a message in your chat log. You can run a rebuild to revive any dead NPCs.");
		}
	}
}
