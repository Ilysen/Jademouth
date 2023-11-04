using XRL;
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
	}
}
