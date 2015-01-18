using System.Collections.Generic;

namespace com.SonOfSofaman.Urbaneer
{
	public class State
	{
		public double ElapsedSeconds { get; private set; }
		public List<Zone> Zones { get; private set; }

		public State()
		{
			this.ElapsedSeconds = 0.0;
			this.Zones = new List<Zone>();
		}

		public void Update(double deltaSeconds)
		{
			this.ElapsedSeconds += deltaSeconds;
		}
	}
}
