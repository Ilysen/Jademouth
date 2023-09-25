namespace XRL.World.Conversations
{
	[HasConversationDelegate]
	public static class Ilysen_Jademouth_ConversationDelegates
	{
		/// <summary>
		/// Checks if the context target's current cell is illuminated with <b>normal light.</b> Night vision and the like won't apply.<br/>
		/// Used in Bright's dialogue to show a separate intro for people who have a light source equipped, since she's sensitive to bright lights.
		/// </summary>
		[ConversationDelegate(Speaker = true)]
		public static bool IfIlluminated(DelegateContext Context) => Context.Target.CurrentCell.GetLight() == XRL.World.LightLevel.Light;
	}
}
