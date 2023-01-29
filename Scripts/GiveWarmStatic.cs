using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	/// <summary>
	/// Helper part used in Chaos Theory for giving Bright a dram of warm static.
	/// </summary>
	public class GiveWarmStatic : IConversationPart
	{
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnterElementEvent.ID;
		}

		public override bool HandleEvent(EnterElementEvent E)
		{
			if (The.Player.GetFreeDrams("warmstatic", null, null, null, false) < 1)
			{
				Popup.Show("You have no warm static to give.", true, true, true, true);
				return false;
			}
			The.Player.UseDrams(3, "warmstatic");
			Popup.Show("You hand the warm static to Bright.", true, true, true, true);
			return base.HandleEvent(E);
		}
	}
}
