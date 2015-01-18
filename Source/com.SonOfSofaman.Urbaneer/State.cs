namespace com.SonOfSofaman.Urbaneer
{
	public class State
	{
		public double ElapsedSeconds { get; private set; }

		public State()
		{
			this.ElapsedSeconds = 0.0;
		}

		public void Update(double deltaSeconds)
		{
			this.ElapsedSeconds += deltaSeconds;
		}
	}
}
