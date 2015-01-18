namespace com.SonOfSofaman.Urbaneer
{
	public class Simulator
	{
		public bool IsAcceptingCommands { get; private set; }

		public Simulator()
		{
			this.IsAcceptingCommands = true;
		}

		public void Exit()
		{
			this.IsAcceptingCommands = false;
		}
	}
}
