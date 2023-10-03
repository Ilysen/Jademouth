using XRL;
using XRL.Wish;

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
	}
}
